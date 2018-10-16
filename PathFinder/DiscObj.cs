using System;
using System.IO;

namespace PathFinder
{
    public abstract class DiscObj
    {
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }

        protected DiscObj(string path)
        {
            Path = path;
            Date = File.GetCreationTime(path);
        }
    }
}
