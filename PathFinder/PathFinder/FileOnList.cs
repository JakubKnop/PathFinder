using System;
using System.IO;
using System.Linq;

namespace PathFinder
{
    public class FileOnList
    {
        public long Size { get; set; }
        public string FullPath { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public bool IsDirectory { get; set; }
        public FileAttributes Attribute { get; set; }
        public FileOnList(string path)
        {
            this.FullPath = path;
            this.Name = path.Split('\\').Last();
            this.Date = File.GetCreationTime(path);
            this.Attribute = File.GetAttributes(this.FullPath);
            if(this.Attribute == FileAttributes.Directory)
            {
                this.IsDirectory = true;
            }
            else
            {
                this.IsDirectory = false;
            }
            if (this.IsDirectory)
            {
                MyDir tmp = new MyDir(this.FullPath, true);
                this.Size = tmp.Size;
            }
            else
            {
                MyFile tmp = new MyFile(this.FullPath);
                this.Size = tmp.Size;
            }
        }
    }
}
