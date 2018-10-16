using System;
using System.IO;

namespace PathFinder
{
    public class MyFile : DiscObj, IPrintable, IBetterComparable<MyFile>
    {
        public long Size { get; set; }
        public string Path { get; set; }
        public DateTime CreationDate { get; set; }
        public MyFile(string name) : base(name)
        {
            this.Path = name;
            this.CreationDate = File.GetCreationTime(this.Path);
            this.Size = new FileInfo(this.Path).Length;
        }
        public void Print()
        {
            Console.WriteLine(this.Path + " " + "Date of creation: " + this.CreationDate + " " + "Bytes: " + this.Size);
        }
        public int CompareTo(MyFile fileToCompare, Enums.ComparisonType comparisonType = Enums.ComparisonType.DateOfCreation)
        {
            if (comparisonType == Enums.ComparisonType.Size)
            {
                if (this.Size > fileToCompare.Size)
                {
                    return 1;
                }
                else if (this.Size < fileToCompare.Size)
                {
                    return -1;
                }
                else if (this.Size == fileToCompare.Size)
                {
                    return 0;
                }
            }
            else if (comparisonType == Enums.ComparisonType.DateOfCreation)
            {
                if (DateTime.Compare(this.CreationDate, fileToCompare.CreationDate) > 0)
                {
                    return 1;
                }
                else if (DateTime.Compare(this.CreationDate, fileToCompare.CreationDate) < 0)
                {
                    return -1;
                }
                else if (DateTime.Compare(this.CreationDate, fileToCompare.CreationDate) == 0)
                {
                    return 0;
                }
            }
            throw new ArgumentOutOfRangeException("Wrong comparison type");
        }
    }
}
