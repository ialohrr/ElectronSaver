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
        public bool isNew { get; }
        public long size { get; }
        public FileToCopy(string name, string path, string Dest, DateTime LastModif, bool isNew, long size)
        {
            this.name = name;
            this.Sourcepath = path;
            DestPath = Dest;
            lastModified = LastModif;
            this.isNew = isNew;
            this.size = size;
            selected = true;
        }
    }
}
