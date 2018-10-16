using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    public class MyDir : DiscObj, IPrintable, IBetterComparable<MyDir>
    {
        public string Path { get; set; }
        public List<DiscObj> Content { get; set; }
        public DateTime CreationDate { get; set; }
        public long Size { get; set; }
        public MyDir(string dir, bool isNested) : base(dir)
        {
            this.Path = dir;
            this.CreationDate = File.GetCreationTime(this.Path);
            this.Size = this.DirectorySize(dir);
            if (isNested)
            {
                this.Save();
            }
        }
        public int DirectorySize(string p)
        {
            string[] a = Directory.GetFiles(p, "*.*");
            int b = 0;
            foreach (string name in a)
            {
                b += 1;
            }
            string[] c = Directory.GetDirectories(p);
            foreach (string l in c)
            {
                b += 1;
            }
            return b;
        }
        public void Save()
        {
                this.Content = new List<DiscObj>();
                foreach (string l in Directory.GetFiles(this.Path))
                {
                    DiscObj d = new MyFile(l);
                    this.Content.Add(d);
                }
                foreach (string k in Directory.GetDirectories(this.Path))
                {
                    DiscObj x = new MyDir(k, false);
                    this.Content.Add(x);
                }
            
        }
        public void Print()
        {
            Console.WriteLine(this.Path + " " + "Date of creation: " + this.CreationDate + " " + "Files: " + this.Size);
            foreach (IPrintable s in this.Content)
            {
                s.Print();
            }
        }
        public int CompareTo(MyDir a, Enums.ComparisonType C = Enums.ComparisonType.Size)
        {
            if (C == Enums.ComparisonType.Size)
            {
                if (this.Size > a.Size)
                {
                    return 1;
                }
                else if (this.Size < a.Size)
                {
                    return -1;
                }
                else if (this.Size == a.Size)
                {
                    return 0;
                }
            }
            else if (C == Enums.ComparisonType.DateOfCreation)
            {
                if (DateTime.Compare(this.CreationDate, a.CreationDate) > 0)
                {
                    return 1;
                }
                else if (DateTime.Compare(this.CreationDate, a.CreationDate) < 0)
                {
                    return -1;
                }
                else if (DateTime.Compare(this.CreationDate, a.CreationDate) == 0)
                {
                    return 0;
                }
            }
            throw new ArgumentOutOfRangeException("Wrong comparison type");
        }
        public MyDir MaxDir(Enums.ComparisonType compType)
        {
            IEnumerable<MyDir> a = this.Content.OfType<MyDir>();
            MyDir max = Program.Max(a, compType);
            foreach (MyDir s in a)
            {
                MyDir x = s.MaxDir(compType);
                if (x != null && x.CompareTo(max, compType) == 1)
                {
                    max = x;
                }
            }
            return max;
        }
        public MyFile MaxFile(Enums.ComparisonType compType)
        {
            IEnumerable<MyDir> b = this.Content.OfType<MyDir>();
            IEnumerable<MyFile> a = this.Content.OfType<MyFile>();
            MyFile max = Program.Max(a, compType);
            foreach (MyDir s in b)
            {
                MyFile x = s.MaxFile(compType);
                if (x != null && max != null)
                {
                    if (x.CompareTo(max, compType) == 1)
                    {
                        max = x;
                    }
                }
                else if (max == null)
                {
                    max = x;
                }
            }
            return max;
        }
    }
}

