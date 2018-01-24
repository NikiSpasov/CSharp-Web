namespace DownloadFileAsync
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            //this is "classic" method with Task, async and await + GetAwaiter() and GetResult()!!!
            //Task.Run(async () =>
            //{
            //   await DownloadFileAsync();
            //})
            //.GetAwaiter()
            //.GetResult();

            //example with GetStringAsync:

            Task
                .Run(async () =>
            {
                var htpClient = new HttpClient();
                var result = await htpClient.GetStringAsync("http://dir.bg");
                Console.WriteLine(result);
            })
            .GetAwaiter()
            .GetResult();

        }

        public static async Task DownloadFileAsync()
        {
            var webclient = new WebClient();

            Console.WriteLine("Downloading...");

            await webclient.DownloadFileTaskAsync("https://i.ytimg.com/vi/sLnC4fjSKdc/maxresdefault.jpg", "cat.jpg");

            Console.WriteLine("Finished!");
        }
    }
}
