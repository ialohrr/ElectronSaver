using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SauvegardeElliot
{
    /// <summary>
    /// Logique d'interaction pour AddFolderWindow.xaml
    /// </summary>
    public partial class AddFolderWindow : Window
    {
        public static ObservableCollection<string> FileListTIgnore;
        public AddFolderWindow()
        {
            InitializeComponent();
            FileListTIgnore = new ObservableCollection<string>();
            FileIgnoreListBx.ItemsSource = FileListTIgnore;
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

        public string FolderName
        {
            get { return Nametxt.Text; }
        }

        public string FolderSrc
        {
            get { return SourceTxt.Text; }
        }

        public string FolderDest
        {
            get { return DestTxt.Text; }
        }


        private void ValidCLick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void IgnoreCLck(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDlg = new System.Windows.Forms.OpenFileDialog();
            openFileDlg.Multiselect = true;
            openFileDlg.Title = "Selectionner un ou des fichers a ignorer";
            openFileDlg.InitialDirectory = FolderSrc;

            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                foreach(string path in openFileDlg.FileNames)
                FileListTIgnore.Add(path.Replace(FolderSrc,""));
            }
        }
    }
}
