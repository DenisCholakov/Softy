using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SUS.HTTP.Interfaces;
using SUS.MvcFramework;

namespace SUS.HTTP
{
    public class HttpServer : IHttpServer
    {
        private readonly List<Route> routeTable;

        public HttpServer(List<Route> routeTable)
        {
            this.routeTable = routeTable;
        }

        public async Task StartAsync(int port)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback,  port);

            tcpListener.Start();

            while (true)
            {
               TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
               ProcessClientAsync(tcpClient);
            }
        }

        private async Task ProcessClientAsync(TcpClient tcpClient)
        {
            try
            {
                await using NetworkStream stream = tcpClient.GetStream();

                int position = 0;
                byte[] buffer = new byte[HTTPConstants.BufferSize];
                List<byte> data = new List<byte>();

                while (true)
                {
                    int count = await stream.ReadAsync(buffer, 0, buffer.Length);
                    position += count;

                    if (count < buffer.Length)
                    {
                        var bufferWithData = new byte[count];
                        Array.Copy(buffer, bufferWithData, count);
                        data.AddRange(bufferWithData);

                        break;
                    }
                    else
                    {
                        data.AddRange(buffer);
                    }
                }

                var requestAsString = Encoding.UTF8.GetString(data.ToArray());

                var request = new HttpRequest(requestAsString);
                Console.WriteLine($"{request.Method} {request.Path} => {request.Headers.Count} headers");

                HttpResponse response;
                var route = this.routeTable.FirstOrDefault(
                    x => String.Compare(x.Path, request.Path, StringComparison.OrdinalIgnoreCase) == 0
                    && x.Method == request.Method);

                if (route != null)
                {
                    response = route.Action(request);
                }
                else
                {
                    response = new HttpResponse("text/html", new byte[0], HttpStatusCode.NotFound);
                }

                var responseHeaderBytes = Encoding.UTF8.GetBytes(response.ToString());

                await stream.WriteAsync(responseHeaderBytes, 0, responseHeaderBytes.Length);
                await stream.WriteAsync(response.Body, 0, response.Body.Length);

                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
