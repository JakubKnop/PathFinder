using System;
using System.Collections.Generic;
using System.Linq;

namespace PathFinder
{
    class Program
    {
        public static T Max<T>(IEnumerable<T> x, Enums.ComparisonType compType) where T : IBetterComparable<T>
        {
            T w = x.First();
            foreach (T s in x)
            {
                if (w.CompareTo(s, compType) == -1)
                {
                    w = s;
                }
            }
            return w;
        }
        static void Main(string[] args)
        {
            FileController.GetDirectoryContent(@"C:\Users\knop\Desktop\testsource");
            Console.ReadKey();
        }
    }
}
