using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SauvegardeElliot
{
    public class FoldertoSave : INotifyPropertyChanged
    {
        private string _name;
        public string name { get { return _name; } set { _name = value; RaiseProperChanged(); } }

        private string _src;
        public string src { get { return _src; } set { src = value; RaiseProperChanged(); } }

        private string _dest;

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
