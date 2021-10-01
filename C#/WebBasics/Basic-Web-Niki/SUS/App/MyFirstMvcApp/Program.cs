using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using SUS.HTTP.Interfaces;
using SUS.MvcFramework;

namespace MyFirstMvcApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new StartUp(), 80);
        }

    }
}
