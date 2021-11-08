using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ElectronSave
{
    class FileCopier
    {
        private FoldertoSave Folder;
        public List<FileToCopy> toCopies { get; set; }

        public FileCopier(FoldertoSave foldertosave, bool verifiModif)
        {
            Folder = foldertosave;
            toCopies = new();
            try
            {
                if (verifiModif)
                    listModif();
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
        }

        public void CopyFile()
        {
            try
            {
                foreach (FileToCopy file in toCopies)
                {
                    if (file.selected)
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(file.DestPath)))
                            Directory.CreateDirectory(Path.GetDirectoryName(file.DestPath));
                        File.Copy(file.Sourcepath, file.DestPath, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckEnoughSpace()
        {
            try
            {
                long cumulativeSourceSize = 0;
                long cumulativeDestSize = 0;
                foreach (FileToCopy file in toCopies)
                {
                    cumulativeSourceSize += file.size;
                    if (!file.isNew)
                        cumulativeDestSize = new FileInfo(file.DestPath).Length;
                }

                long totalNeededSpace = cumulativeSourceSize - cumulativeDestSize;
                if (totalNeededSpace > GetDiskFreeSpace(toCopies[0].DestPath))
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        void listModif()
        {
            try
            {
                string[] filePathsInSrc = Directory.GetFiles(Folder.src, "*.*", SearchOption.AllDirectories);
                foreach (string SubFolderPath in Folder.FolderToIgnore)
                {
                    string[] FilePathToIgnore = Directory.GetFiles(Path.GetFullPath(SubFolderPath, Folder.src), "*.*", SearchOption.AllDirectories);
                    filePathsInSrc = filePathsInSrc.Except(FilePathToIgnore).ToArray();
                }

                string[] EsPathToIgnore = Directory.GetFiles(Path.GetFullPath(".Es", Folder.src), "*.*", SearchOption.AllDirectories);
                filePathsInSrc = filePathsInSrc.Except(EsPathToIgnore).ToArray();

                bool ToCopy = false;


                foreach (string file in filePathsInSrc)
                {
                    string RelativePath = Path.GetRelativePath(Folder.src, file);

                    if (CheckIfIgnored(RelativePath))
                        continue;

                    string DestFullPath = Path.GetFullPath(RelativePath, Folder.dest);

                    var SrcFilesize = new FileInfo(file).Length;
                    var SrcLastModif = File.GetLastWriteTime(file);
                    bool isNew = true;

                    if (File.Exists(DestFullPath))
                    {
                        isNew = false;
                        var DestFileSize = new FileInfo(DestFullPath).Length;
                        var DestLastModif = File.GetLastWriteTime(DestFullPath);
                        if ((SrcFilesize != DestFileSize) || (SrcLastModif != DestLastModif))
                            ToCopy = true;
                    }
                    else
                    {
                        ToCopy = true;
                    }

                    if (ToCopy)
                    {
                        toCopies.Add(new FileToCopy(Path.GetFileName(file), file, DestFullPath, File.GetLastWriteTime(file), isNew, SrcFilesize));
                    }
                    ToCopy = false;
                }

            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        public static long GetDiskFreeSpace(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            ulong dummy = 0;

            if (!GetDiskFreeSpaceEx(path, out ulong freeSpace, out dummy, out dummy))
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            return (long)freeSpace;
        }

        public void CopyFilesRecursively(FoldertoSave Folder)
        {
            try
            {
                foreach (string dirPath in Directory.GetDirectories(Folder.src, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(Folder.src, Folder.dest));
                }

                foreach (string newPath in Directory.GetFiles(Folder.src, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(Folder.src, Folder.dest), true);
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        bool CheckIfIgnored(string path)
        {

            if (Folder.FileToIgnore.Contains(Path.GetFileName(path)))
                return true;

            return false;
        }
    }
}
