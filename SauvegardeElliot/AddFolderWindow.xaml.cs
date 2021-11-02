using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ElectronSave
{
    /// <summary>
    /// Logique d'interaction pour AddFolderWindow.xaml
    /// </summary>
    public partial class AddFolderWindow : Window
    {
        public static ObservableCollection<string> FileListToIgnore;
        public static ObservableCollection<string> FolderIgnoreList;
        public FoldertoSave Folder;
        public AddFolderWindow()
        {
            InitializeComponent();
            FileListToIgnore = new ObservableCollection<string>();
            FolderIgnoreList = new ObservableCollection<string>();
            FileIgnoreListBx.ItemsSource = FileListToIgnore;
            FolderIgnoreListbx.ItemsSource = FolderIgnoreList;
        }

        public AddFolderWindow(FoldertoSave Folder)
        {
            InitializeComponent();
            this.Folder = Folder;
            FileListToIgnore = new ObservableCollection<string>(Folder.FileToIgnore);
            FolderIgnoreList = new ObservableCollection<string>(Folder.FolderToIgnore);
            FileIgnoreListBx.ItemsSource = FileListToIgnore;
            FolderIgnoreListbx.ItemsSource = FolderIgnoreList;
            SourceTxt.Text = Folder.src;
            Nametxt.Text = Folder.name;
            DestTxt.Text = Folder.dest;
        }

        private void SrcSelect(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                SourceTxt.Text = openFileDlg.SelectedPath;
            }
        }

        private void DestClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                DestTxt.Text = openFileDlg.SelectedPath;
            }
        }

        private void AnnulerClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }


        private void ValidCLick(object sender, RoutedEventArgs e)
        {
            Folder = new FoldertoSave(Nametxt.Text, SourceTxt.Text, DestTxt.Text, FileListToIgnore, FolderIgnoreList);
            DialogResult = true;
            this.Close();
        }

        private void IgnoreFileClick(object sender, RoutedEventArgs e)
        {
            if (SourceTxt.Text != "")
            {
                System.Windows.Forms.OpenFileDialog openFileDlg = new System.Windows.Forms.OpenFileDialog();
                openFileDlg.Multiselect = true;
                openFileDlg.Title = "Selectionner un ou des fichiers a ignorer";
                openFileDlg.InitialDirectory = SourceTxt.Text;

                var result = openFileDlg.ShowDialog();
                if (result.ToString() != string.Empty)
                {
                    foreach (string path in openFileDlg.FileNames)
                    {
                        FileListToIgnore.Add(Path.GetFileName(path));
                    }
                }
            }
        }


        private void SupprFileClick(object sender, RoutedEventArgs e)
        {
            List<object> items = new List<object>((IEnumerable<object>)FileIgnoreListBx.SelectedItems);
            if (items != null)
            {
                foreach (var item in items)
                    FileListToIgnore.Remove((string)item);
            }
        }

        private void IgnoreFoldClick(object sender, RoutedEventArgs e)
        {
            if (SourceTxt.Text != "")
            {
                System.Windows.Forms.FolderBrowserDialog FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();

                FolderBrowser.Description = "Selectionner un dossier a ignorer";

                var result = FolderBrowser.ShowDialog();
                if (result.ToString() != string.Empty && result == System.Windows.Forms.DialogResult.OK)
                {
                    FolderIgnoreList.Add(Path.GetRelativePath(SourceTxt.Text, FolderBrowser.SelectedPath));
                }
            }
        }

        private void SupprFoldClick(object sender, RoutedEventArgs e)
        {
            List<object> items = new List<object>((IEnumerable<object>)FolderIgnoreListbx.SelectedItems);

            if (items != null)
            {
                foreach (var item in items)
                    FolderIgnoreList.Remove((string)item);
            }
        }

    }
}
