namespace WebUtilityTool
{
    using System;
    using System.Net;
    using System.Net.Http.Headers;

    public class WebUtilityTool
    {
        public static void Main()
        {
            var url = "https://softuni.bg/trainings/1736/c-sharp-web-development-basics-september-2017#lesson-6465";
            var decodedUrl = WebUtility.UrlDecode(url);

            //Console.WriteLine(decodedUrl);

            var parsedUrl = new Uri(url);

            Console.WriteLine(parsedUrl.Scheme);
            Console.WriteLine(parsedUrl.Host);
            Console.WriteLine(parsedUrl.Port);
            Console.WriteLine(parsedUrl.AbsolutePath);
            Console.WriteLine(parsedUrl.Query);
            Console.WriteLine(parsedUrl.Fragment);

           
        }
    }
}
