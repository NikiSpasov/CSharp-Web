namespace MyCoolWebServer.Server
{
    using System;
    using Common;
    using Handlers;
    using HTTP;
    using HTTP.Contracts;
    using Routing.Contracts;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;


    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpContext = new HttpContext(httpRequest);

                var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                var byteSegments = new ArraySegment<byte>(responseBytes);

                await this.client.SendAsync(byteSegments, SocketFlags.None);

                Console.WriteLine($"--------REQUEST---------");
                Console.WriteLine(httpRequest);
                Console.WriteLine($"--------RESPONSE--------");
                Console.WriteLine(httpResponse);
                Console.WriteLine();

            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            string request = string.Empty;
            ArraySegment<byte> data = new ArraySegment<byte>(new byte[1024]);

            int numBytesRead;

            while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            {
                request += Encoding.ASCII.GetString(data.Array, 0, numBytesRead);

                if (numBytesRead < 1024)
                {
                    break;
                }
            }

            if (request.Length == 0)
            {
                return null;
            }
            return new HttpRequest(request);
        }
    }
}