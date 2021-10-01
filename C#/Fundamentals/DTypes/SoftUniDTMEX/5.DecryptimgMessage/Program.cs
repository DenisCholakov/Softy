using System;
using System.Text;

namespace _5.DecryptimgMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            byte decrypter = byte.Parse(Console.ReadLine());
            byte charactersCount = byte.Parse(Console.ReadLine());

            StringBuilder message = new StringBuilder();

            for (int i = 0; i < charactersCount; i++)
            {
                char letter = char.Parse(Console.ReadLine());
                message.Append((char)(letter + decrypter));
            }

            Console.WriteLine(message);
        }
    }
}
