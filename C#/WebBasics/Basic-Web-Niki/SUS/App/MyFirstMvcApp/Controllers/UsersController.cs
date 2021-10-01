using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUS.HTTP;
using SUS.MvcFramework;

namespace MyFirstMvcApp.Controllers
{
    class UsersController : Controller
    {

        public HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        public HttpResponse DoLogin(HttpRequest arg)
        {
            return this.Redirect("/");
        }

        public HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }
    }
}
