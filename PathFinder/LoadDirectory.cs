using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class LoadDirectory
    {
        public MyDir LoadDir()
        {
            while (true)
            {
                try
                {
                    string a = Console.ReadLine();
                    MyDir x = new MyDir(a);
                    Console.WriteLine("dir loaded");
                    return x; 
                }
                catch (Exception)
                {
                    Console.WriteLine("bad path");
                }
            }
        }
    }
}
