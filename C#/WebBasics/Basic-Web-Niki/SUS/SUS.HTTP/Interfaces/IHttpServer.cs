using System;
using System.Threading.Tasks;

namespace SUS.HTTP.Interfaces
{
    public interface IHttpServer
    {
        Task StartAsync(int port);
    }
}
