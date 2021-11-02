using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            if (verifiModif)
                listModif();
        }

        public void CopyFile()
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

        void listModif()
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

                if (File.Exists(DestFullPath))
                {
                    var SrcFilesize = new FileInfo(file).Length;
                    var DestFileSize = new FileInfo(DestFullPath).Length;
                    var SrcLastModif = File.GetLastWriteTime(file);
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
                    toCopies.Add(new FileToCopy(Path.GetFileName(file), file, DestFullPath, File.GetLastWriteTime(file)));
                }
                ToCopy = false;
            }

        }

        public void CopyFilesRecursively(FoldertoSave Folder)
        {
            foreach (string dirPath in Directory.GetDirectories(Folder.src, "*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath.Replace(Folder.src, Folder.dest));
            }

            foreach (string newPath in Directory.GetFiles(Folder.src, "*.*", SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(Folder.src, Folder.dest), true);
            }
        }

        bool CheckIfIgnored(string path)
        {

            //var test = Path.GetDirectoryName(path);
            //var test2 = Path.GetPathRoot(test);
            //if (Folder.FolderToIgnore.Contains(Path.GetPathRoot(test)))
            //    return true;

            if (Folder.FileToIgnore.Contains(Path.GetFileName(path)))
                return true;

            return false;
        }
    }
}
