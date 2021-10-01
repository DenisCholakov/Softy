using System;
using System.IO;

namespace CopyBinaryFile
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream reader = new FileStream("../../../copyMe.png", FileMode.Open))
            {
                using (FileStream writer = new FileStream("../../../copied.png", FileMode.Create))
                {
                    byte[] buffer = new byte[4096];

                    while (reader.Position != reader.Length)
                    {
                        reader.Read(buffer, 0, buffer.Length);

                        writer.Write(buffer, 0, buffer.Length);
                    }
                }
            }
        }
    }
}
