using System;

namespace ExtractFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] filePath = Console.ReadLine().Split('\\');
            string[] file = filePath[filePath.Length - 1].Split('.');
            string fileName = file[0];
            string fileExtension = file[1];

            System.Console.WriteLine($"File name: {fileName}"); ;
            System.Console.WriteLine($"File extension: {fileExtension}"); ;
        }
    }
}
