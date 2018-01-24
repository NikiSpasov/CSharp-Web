namespace _06.DownloadAsyncHtmlnfo
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
			//additional: How to transfer bytes <=> string

			//var text = "Hello, world!";

			//var bytes = Encoding.UTF8.GetBytes(text); //bite array;

			//var stringFromByteArray = Encoding.UTF8.GetString(bytes);


            Task
				.Run(async () =>
				{
					await GetHeaders("https://www.dir.bg");
				})
				.GetAwaiter()
				.GetResult();
            
        }

        public static async Task GetHeaders(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

                var headers = response.Headers;

                foreach (var header in headers)
                {
                    Console.WriteLine(header.Key + ": " + string.Join(", ", header.Value));
                }

                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);
            }
        }
    }
}
