using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ElectronSave
{
    class FolderParamSaver
    {
        public List<FoldertoSave> Folders { get; }
        public FolderParamSaver()
        {
            //DeleteAll();
            Folders = new();
            ReadAllSettings();
        }

        void DeleteAll()
        {
            var appSettings = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = appSettings.AppSettings.Settings;
            settings.Clear();

            appSettings.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(appSettings.AppSettings.SectionInformation.Name);
        }

        public void DeletFolder(FoldertoSave Folder)
        {
            var appSettings = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = appSettings.AppSettings.Settings;
            if (settings[Folder.name] != null)
                settings.Remove(Folder.name);

            appSettings.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(appSettings.AppSettings.SectionInformation.Name);
        }

        public void SaveConfig(FoldertoSave Folder)
        {
            string path = Path.GetFullPath(".Es", Folder.src);
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            var FolderParam = new XElement("FolderConfig",
                    new XElement("Name", Folder.name),
                    new XElement("Source", Folder.src),
                    new XElement("Destination", Folder.dest),
                    new XElement("FilesToIgnore"),
                    new XElement("FoldersToIgnore"));

            XElement FilesToIgnore = FolderParam.Element("FilesToIgnore");
            foreach (string file in Folder.FileToIgnore)
                FilesToIgnore.Add(new XElement("File", file));

            XElement FoldersToIgnore = FolderParam.Element("FoldersToIgnore");
            foreach (string folder in Folder.FolderToIgnore)
                FoldersToIgnore.Add(new XElement("Folder", folder));

            string FilePath = Path.GetFullPath(Folder.name, path);
            FolderParam.Save(FilePath);
            AddUpdateAppSettings(Folder.name, FilePath);

        }

        void ReadAllSettings()
        {
            string EsfilePath = "";
            try
            {
                var appSettings = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                if (appSettings.AppSettings.Settings.Count > 0)
                {
                    foreach (string key in appSettings.AppSettings.Settings.AllKeys)
                    {
                        EsfilePath = appSettings.AppSettings.Settings[key].Value;
                        try
                        {
                            XElement FolderConfig = XElement.Load(EsfilePath);
                            string FolderName = FolderConfig.Element("Name").Value;
                            string FolderSource = FolderConfig.Element("Source").Value;
                            string FolderDest = FolderConfig.Element("Destination").Value;
                            var Xelemfiletoignore = FolderConfig.Element("FilesToIgnore").Elements("File").ToList();
                            List<string> FilesToIgnore = new();
                            var Xelemfoldertoignore = FolderConfig.Element("FoldersToIgnore").Elements("Folder").ToList();
                            List<string> FoldersToIgnore = new();

                            foreach (var file in Xelemfiletoignore)
                                FilesToIgnore.Add(file.Value);
                            foreach (var folder in Xelemfoldertoignore)
                                FoldersToIgnore.Add(folder.Value);

                            Folders.Add(new FoldertoSave(FolderName, FolderSource, FolderDest, FilesToIgnore, FoldersToIgnore));

                        }
                        catch (Exception)
                        {

                            appSettings.AppSettings.Settings.Remove(key);
                            appSettings.Save(ConfigurationSaveMode.Modified);
                            ConfigurationManager.RefreshSection(appSettings.AppSettings.SectionInformation.Name);
                        }

                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var appSettings = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = appSettings.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                appSettings.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(appSettings.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}
