using System;
using System.IO;
using System.Linq;

namespace CollectionIteration
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string command = Console.ReadLine();
            string[] createCommand = command.Split().Skip(1).ToArray();
            ListyIterator<string> li = new ListyIterator<string>(createCommand);

            while ((command = Console.ReadLine()) != "END")
            {
                try
                {
                    switch (command)
                    {
                        case "Move":
                            Console.WriteLine(li.Move());
                            break;
                        case "Print":
                            li.Print(); 
                            break;
                        case "PrintAll": Console.WriteLine(String.Join(' ', li));
                            break;
                        case "HasNext":
                            Console.WriteLine(li.HasNext());
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
