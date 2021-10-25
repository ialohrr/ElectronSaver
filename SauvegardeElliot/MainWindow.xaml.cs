using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SauvegardeElliot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Folders = new ObservableCollection<FoldertoSave>();
            FolderLIst.ItemsSource = Folders;
            FolderCBx.ItemsSource = Folders;
        }

        public static ObservableCollection<FoldertoSave> Folders;

        private void AddClick(object sender, RoutedEventArgs e)
        {
            AddFolderWindow addFolder = new AddFolderWindow();
            bool? result = addFolder.ShowDialog();
            if (result == true)
                Folders.Add(new FoldertoSave(addFolder.FolderName, addFolder.FolderSrc, addFolder.FolderDest));
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
                    Folders.Remove((FoldertoSave)FolderLIst.SelectedItem);
                }
            }
        }
    }
}
