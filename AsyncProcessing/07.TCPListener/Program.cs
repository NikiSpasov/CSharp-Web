using System;

namespace _07.TCPListener
{
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            int port = 1337;
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

            TcpListener tcpListener = new TcpListener(ipAddress, port);

            tcpListener.Start();

            Task
                .Run(async () => await Connect(tcpListener))
                .GetAwaiter()
                .GetResult();
        }

        private static async Task Connect(TcpListener listener)
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();

                var buffer = new byte[1024];
                await client.GetStream().ReadAsync(buffer, 0, buffer.Length);

                var clientMessage = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(clientMessage);

                var responseMessage = "HTTP/1.1 200 OK\nContent-Type: text/plain\n\nHello from server!\n";
                var data = Encoding.UTF8.GetBytes(responseMessage);
                await client.GetStream().WriteAsync(data, 0, data.Length);
                
                client.Dispose();
            }
        }
    }
}
