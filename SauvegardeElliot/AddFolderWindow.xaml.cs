using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SauvegardeElliot
{
    /// <summary>
    /// Logique d'interaction pour AddFolderWindow.xaml
    /// </summary>
    public partial class AddFolderWindow : Window
    {

        public AddFolderWindow()
        {
            InitializeComponent();
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
    }
}
