using System;

namespace ElectronSave
{
    public class FileToCopy
    {
        public string name { get; }
        public string Sourcepath { get; }
        public string DestPath { get; }
        public DateTime lastModified { get; }
        public bool selected { get; set; }

        public FileToCopy(string name, string path, string Dest, DateTime LastModif)
        {
            this.name = name;
            this.Sourcepath = path;
            DestPath = Dest;
            lastModified = LastModif;
            selected = true;
        }
    }
}
