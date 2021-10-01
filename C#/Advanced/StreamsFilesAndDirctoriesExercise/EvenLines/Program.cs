using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace EvenLines
{
    class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader("../../../text.txt"))
            {
                using (StreamWriter writer = new StreamWriter("../../../evenLines.txt"))
                {
                    string line = reader.ReadLine();
                    int counter = 0;

                    while (line != null)
                    {

                        if (counter % 2 == 0)
                        {
                            Regex pattern = new Regex("[-,.!?]");
                            line = String.Join(' ', pattern.Replace(line, "@").Split().ToArray().Reverse());
                            writer.WriteLine(line);
                        }

                        line = reader.ReadLine();
                        counter++;
                    }
                }
            }
        }
    }
}
