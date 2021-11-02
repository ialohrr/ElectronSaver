using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElectronSave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ParamSaver = new();
            Folders = new(ParamSaver.Folders);
            //foreach (FoldertoSave folder in ParamSaver.Folders)
            //    Folders.Add(folder);
            FolderLIst.ItemsSource = Folders;
            FolderCBx.ItemsSource = Folders;
        }



        private static ObservableCollection<FoldertoSave> Folders;
        private static FolderParamSaver ParamSaver;
        private Task copy;

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddFolderWindow addFolder = new();
            bool? result = addFolder.ShowDialog();
            if (result == true)
            {
                Folders.Add(addFolder.Folder);
                ParamSaver.SaveConfig(addFolder.Folder);
            }
        }

        private void deleteClick(object sender, RoutedEventArgs e)
        {
            var item = FolderLIst.SelectedItem;
            if (item != null)
            {
                string CourseName = (FolderLIst.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to delete the folder " + CourseName + "?");
                if (result == MessageBoxResult.OK)
                {
                    ParamSaver.DeletFolder((FoldertoSave)item);
                    Folders.Remove((FoldertoSave)item);
                }
            }
        }

        private void ModifyClick(object sender, RoutedEventArgs e)
        {
            var item = (FoldertoSave)FolderLIst.SelectedItem;

            ModifyFolder(item);
        }

        public FoldertoSave GetFolderByName(string name)
        {
            return Folders.Where(Folder => Folder.name == name).First();
        }
        public void ModifyFolder(FoldertoSave item)
        {
            if (item != null)
            {
                var index = Folders.IndexOf(item);
                AddFolderWindow addFolder = new(item);
                bool? result = addFolder.ShowDialog();
                if (result == true)
                {
                    Folders.Remove(item);
                    Folders.Insert(index, addFolder.Folder);
                    ParamSaver.DeletFolder(addFolder.Folder);
                    ParamSaver.SaveConfig(addFolder.Folder);
                }
            }
        }

        private void SaveFileClick(object sender, RoutedEventArgs e)
        {
            var SelectedFolder = (FoldertoSave)FolderCBx.SelectedItem;
            SaveFiles(SelectedFolder);
        }

        public void SaveFiles(FoldertoSave SelectedFolder)
        {
            if (SelectedFolder != null)
            {
                FileCopier fileCopier = new(SelectedFolder, true);

                if (fileCopier.toCopies.Count > 0)
                {
                    ReviewFile ReviewWindow = new ReviewFile(fileCopier.toCopies);
                    bool? result = ReviewWindow.ShowDialog();
                    if (result != null && result == true)
                    {
                        fileCopier.toCopies = ReviewWindow.FilesToCopy;
                        copy = new Task(() => fileCopier.CopyFile());
                        copy.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Le dossier de sauvegarde est à jour");
                }
            }
        }

        private void RestoreClick(object sender, RoutedEventArgs e)
        {
            FoldertoSave SelectedFolder = (FoldertoSave)FolderCBx.SelectedItem;
            RestoreFiles(SelectedFolder);
        }

        public void RestoreFiles(FoldertoSave SelectedFolder)
        {
            if (SelectedFolder != null)
            {
                var result = MessageBox.Show("Êtes vous sur de vouloir remplacer tous les fichier du dossier " + SelectedFolder.name
                                                             + " avec ceux sauvegarder dans le dossier " + SelectedFolder.dest, "Restoration", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.OK)
                {
                    FileCopier fileCopier = new(SelectedFolder, false);
                    copy = new Task(() => fileCopier.CopyFilesRecursively(SelectedFolder));
                    copy.Start();
                }

            }
        }

        private void OnClose(object sender, System.EventArgs e)
        {
            copy?.Wait();
        }
    }
}
