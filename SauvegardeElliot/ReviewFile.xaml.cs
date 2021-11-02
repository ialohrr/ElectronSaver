using System.Collections.Generic;
using System.Windows;

namespace ElectronSave
{
    /// <summary>
    /// Logique d'interaction pour ReviewFile.xaml
    /// </summary>
    public partial class ReviewFile : Window
    {
        public List<FileToCopy> FilesToCopy { get; }
        public ReviewFile(List<FileToCopy> FilesToCopy)
        {
            InitializeComponent();
            this.FilesToCopy?.Clear();
            this.FilesToCopy = FilesToCopy;
            FileLIst.ItemsSource = this.FilesToCopy;
        }

        private void ValidClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
