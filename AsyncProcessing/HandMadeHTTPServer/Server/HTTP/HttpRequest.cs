namespace _08.HandMadeHTTPServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using _08.HandMadeHTTPServer.Server.Enums;
    using _08.HandMadeHTTPServer.Server.Exeptions;
    using _08.HandMadeHTTPServer.Server.HTTP.Contracts;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();

            this.ParseRequest(requestString);
        }

        public Dictionary<string, string> FormData { get; }

        public HttpHeaderCollection HeaderCollection { get; }

        public string Path { get; set; }

        public Dictionary<string, string> QueryParameters { get; }

        public HttpRequestMethod? RequestMethod{ get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> UrlParameters { get; }

        public void AddUrlParameter(string key, string value)
        {
            throw new System.NotImplementedException();
        }

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            string[] requestLine = requestLines[0].Trim()
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException("Invalid request line");
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            if (this.RequestMethod == null)
            {
                throw new BadRequestException("Invalid request line");
            }
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] {'?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseParameters();

            if (this.RequestMethod == HttpRequestMethod.POST)
            {
                this.ParseQuery(requestLines[requestLines.Length -1], this.FormData);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains("/"))
            {
                return;
            }

            string query = this.Url.Split('?')[1];
            this.ParseQuery(query, this.QueryParameters);
        }

        private void ParseQuery(string query, Dictionary<string, string> dict)
        {
            if (!query.Contains("="))
            {
                return;
            }

            string[] queryPairs = query.Split('&');

            foreach (var queryPair in queryPairs)
            {
                string[] queryArgs = queryPair.Split('*'); //??
                if (queryArgs.Length != 2)
                {
                    continue;
                }

                dict.Add(
                    WebUtility.UrlDecode(queryArgs[0]),
                    WebUtility.UrlDecode(queryArgs[1]));
            }
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);
            for (int i = 1; i < endIndex; i++)
            {
                string[] headerArgs = requestLines[i]
                    .Split(new[] { ": "}, StringSplitOptions.None);

                var httpHeader = new HttpHeader(headerArgs[0], headerArgs[1]); //check for repetitions?

                this.HeaderCollection.Add(httpHeader);
            }

            if (!this.HeaderCollection.ContainsKey("Host"))
            {
                throw new BadRequestException("Invalid request line");
            }
        }

        private HttpRequestMethod ParseRequestMethod(string method)
        {
            HttpRequestMethod result;
            bool success = Enum.TryParse(method, true, out result);
            if (success)
            {
                return result;
            }
            return result;
        }
    }
}
