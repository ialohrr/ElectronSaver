using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ElectronSave
{
    public class FoldertoSave : INotifyPropertyChanged
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; RaiseProperChanged(); } }

        private string _src;
        public string src { get { return _src; } set { src = value; RaiseProperChanged(); } }

        private string _dest;


        public List<string> FileToIgnore { get; }
        public List<string> FolderToIgnore { get; }

        public FoldertoSave(string name, string source, string dest, ObservableCollection<string> FilePathToIgnore, ObservableCollection<string> SubFOlderToIgnore)
        {
            _name = name;
            _src = source;
            _dest = dest;
            FileToIgnore = new List<string>(FilePathToIgnore);
            FolderToIgnore = new List<string>(SubFOlderToIgnore);
        }

        public FoldertoSave(string name, string source, string dest, List<string> FilePathToIgnore, List<string> SubFOlderToIgnore)
        {
            _name = name;
            _src = source;
            _dest = dest;
            FileToIgnore = new List<string>(FilePathToIgnore);
            FolderToIgnore = new List<string>(SubFOlderToIgnore);
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
