using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DirectoryTraversal
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Dictionary<string, double>> fileInfo = new Dictionary<string, Dictionary<string, double>>();
            DirectoryInfo directory = new DirectoryInfo("../../../");
            FileInfo[] files = directory.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo currentFile = files[i];

                if (!fileInfo.ContainsKey(currentFile.Extension))
                {
                    fileInfo.Add(currentFile.Extension, new Dictionary<string, double>());
                }

                fileInfo[currentFile.Extension].Add(currentFile.Name, currentFile.Length / 1000.00);
            }

            using (StreamWriter writer = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/DyrectoryTraversal.txt"))
            {
                foreach (var item in fileInfo.OrderByDescending(f => f.Value.Count).ThenBy(i => i.Key))
                {
                    writer.WriteLine(item.Key);

                    foreach (var file in item.Value.OrderByDescending(x => x.Value))
                    {
                        writer.WriteLine($"--{file.Key} - {file.Value}kb");
                    }
                }
            }           
        }
    }
}
