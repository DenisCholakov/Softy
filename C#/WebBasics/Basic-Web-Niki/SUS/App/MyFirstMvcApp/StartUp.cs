using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstMvcApp.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp
{
    public class StartUp : IMvcApplication
    {
        public void ConfigureServices()
        {
        }

        public void Configure(List<Route> routeTable)
        {
            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Get, new UsersController().Login));
            routeTable.Add(new Route("/Users/Login", HttpMethod.Post, new UsersController().DoLogin));
            routeTable.Add(new Route("/Users/Register", HttpMethod.Get, new UsersController().Register));
            routeTable.Add(new Route("/Cards/All", HttpMethod.Get, new CardsController().All));
            routeTable.Add(new Route("/Cards/Add", HttpMethod.Get, new CardsController().Add));
            routeTable.Add(new Route("/Cards/Collection", HttpMethod.Get, new CardsController().Collection));


            routeTable.Add(new Route("/favicon.ico", HttpMethod.Get, new StaticFilesController().Favicon));
            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", HttpMethod.Get, new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/custom.js", HttpMethod.Get, new StaticFilesController().CustomJs));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.js", HttpMethod.Get, new StaticFilesController().BootstrapJS));
        }
    }
}
