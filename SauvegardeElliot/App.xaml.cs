using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace ElectronSave
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            MainWindow mwn = new();

            if (e.Args.Length > 1)
            {
                string EsfilePath = null;
                switch (e.Args[0])
                {
                    case "-m":
                        EsfilePath = Path.GetFullPath(@".Es\FolderParam.xml", e.Args[1]);
                        if (File.Exists(EsfilePath))
                        {
                            XElement FolderConfig = XElement.Load(Path.GetFullPath(EsfilePath));
                            var Folder = mwn.GetFolderByName(FolderConfig.Element("Name").Value);
                            if (Folder != null)
                            {
                                mwn.ModifyFolder(Folder);
                                break;
                            }
                        }
                        mwn.Add.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        break;
                    case "-s":
                        EsfilePath = Path.GetFullPath(@".Es\FolderParam.xml", e.Args[1]);
                        if (File.Exists(EsfilePath))
                        {
                            XElement FolderConfig = XElement.Load(EsfilePath);
                            var Folder = mwn.GetFolderByName(FolderConfig.Element("Name").Value);
                            if (Folder != null)
                            {
                                mwn.SaveFiles(Folder);
                                break;
                            }
                        }
                        break;
                    case "-r":
                        EsfilePath = Path.GetFullPath(@".Es\FolderParam.xml", e.Args[1]);
                        if (File.Exists(EsfilePath))
                        {
                            XElement FolderConfig = XElement.Load(EsfilePath);
                            var Folder = mwn.GetFolderByName(FolderConfig.Element("Name").Value);
                            if (Folder != null)
                            {
                                mwn.RestoreFiles(Folder);
                                break;
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Argument non recconu", "erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
                mwn.Close();
            }
            else
            {
                mwn.Show();
            }
        }

    }
}
