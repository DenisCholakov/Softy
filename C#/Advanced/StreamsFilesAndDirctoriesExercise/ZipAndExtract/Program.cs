using System;
using System.IO.Compression;

namespace ZipAndExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            ZipFile.CreateFromDirectory("../../../", Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/NewFloder");
        }
    }
}
