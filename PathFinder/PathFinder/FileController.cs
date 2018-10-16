using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PathFinder
{
    public class FileController
    {
        public static IEnumerable<FileOnList> GetDirectoryContent(string directoryPath, Enums.ComparisonType compType = Enums.ComparisonType.Default)
        {
            MyDir tmp = new MyDir(directoryPath, true);
            List<FileOnList> discObjList = new List<FileOnList>();
            foreach (DiscObj discObj in tmp.Content)
            {
                discObjList.Add(new FileOnList(discObj.Path));
            }
            return discObjList;
        }
        public static void CopyFiles(IEnumerable<string> srcPaths, string dstPath)
        {
            foreach (string path in srcPaths)
            {
                FileAttributes attribute = File.GetAttributes(path);
                if ((attribute & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    MyDir tmpDir = new MyDir(path, true);
                    Directory.CreateDirectory(Path.Combine(dstPath, tmpDir.Path.Split('\\').Last()));
                    List<string> reqList = new List<string>();
                    foreach (DiscObj discObj in tmpDir.Content)
                    {
                        reqList.Add(discObj.Path);
                        CopyFiles(reqList, Path.Combine(dstPath, discObj.Path.Split('\\').Last()));
                    }
                }
                else
                {
                    MyFile tmpFile = new MyFile(path);
                    File.Create(Path.Combine(dstPath, tmpFile.Path.Split('\\').Last())).Dispose();
                    File.Copy(path, Path.Combine(dstPath, tmpFile.Path.Split('\\').Last()), true);
                }
            }
        }
        public static async Task CopyAsync(IEnumerable<string> srcPaths, string dstPath)
        {
            await Task.Run(() => CopyFiles(srcPaths, dstPath));
        }
    }
}
