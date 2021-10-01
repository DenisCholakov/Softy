using System;
using System.IO;


namespace LineNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("../../../text.txt");
            string[] linesToSave = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                int punctoationMarksCount = 0;
                int lettersCount = 0;

                for (int j = 0; j < currentLine.Length; j++)
                {
                    if (Char.IsPunctuation(currentLine[j]))
                    {
                        punctoationMarksCount++;
                    }
                    else if(Char.IsLetter(currentLine[j]))
                    {
                        lettersCount++;
                    }
                }



                linesToSave[i] = $"Line {i + 1}: {currentLine} ({lettersCount})({punctoationMarksCount})";
            }

            File.WriteAllLines("../../../output.txt", linesToSave);
        }
    }
}
