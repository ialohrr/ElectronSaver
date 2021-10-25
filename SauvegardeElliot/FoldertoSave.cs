using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SauvegardeElliot
{
    public class FoldertoSave : INotifyPropertyChanged
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; RaiseProperChanged(); } }

        private string _src;
        public string src { get { return _src; } set { src = value; RaiseProperChanged(); } }

        private string _dest;

        private ObservableCollection<string> FilePathToIgnore;
        private ObservableCollection<string> SubFOlderToIgnore;

        public FoldertoSave(string name, string source, string dest)
        {
            _name = name;
            _src = source;
            _dest = dest;
        }

        public string dest { get { return _dest; } set { _dest = value; RaiseProperChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
