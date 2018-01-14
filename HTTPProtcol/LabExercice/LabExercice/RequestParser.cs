namespace LabExercice
{
    using System;
    using System.Collections.Generic;

    public class RequestParser
    {
        static void Main()
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var urlParts = line.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                var path = $"/{urlParts[0]}";
                var method = urlParts[1];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add(method);

            }

            var request = Console.ReadLine();
            var requestParts = request.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            var requestedMethod = requestParts[0];
            var requestUrl = requestParts[1];
            var requestProtocol = requestParts[2];


            var responseStatus = 404;
            var responseStatusText = "Not Found";

            if (validUrls.ContainsKey(requestUrl) 
                && validUrls[requestUrl].Contains(requestedMethod.ToLower()))
            {
                responseStatus = 200;
                responseStatusText = "OK";
            }

            Console.WriteLine($"{requestProtocol} {responseStatus} {responseStatusText} ");
            Console.WriteLine($"Content=Length: {responseStatusText.Length}");
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine(responseStatusText);
        }
    }
}
