namespace MyCoolWebServer.Server.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml.Serialization;
    using Enums;
    using Exeptions;
    using Contracts;
    using MyCoolWebServer.Server.Common;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.Http.Contracts;

    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            this.FormData = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.UrlParameters = new Dictionary<string, string>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            
            this.ParseRequest(requestText);
        }

        public IDictionary<string, string> FormData { get; private set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public HttpCookieCollection Cookies { get; private set; }

        public string Path { get; private set; }

        public IDictionary<string, string> QueryParameters { get; private set; }

        public HttpRequestMethod Method { get; private set; }

        public string Url { get; private set; }

        public IDictionary<string, string> UrlParameters { get; private set; }

        public IHttpSession Session { get; set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }

        private void ParseRequest(string requestText)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestText, nameof(requestText));

            var requestLines = requestText.Split(Environment.NewLine);

            if (!requestLines.Any())
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            string[] requestLine = requestLines.First().Split(
                new[] { " " },
                StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                BadRequestException.ThrowFromInvalidRequest();
            }

            this.Method = this.ParseMethod(requestLine.First());
            this.Url = requestLine[1];
            this.Path = this.ParsePath(this.Url);

            this.ParseHeaders(requestLines);
            this.ParseCookies();
            this.SetSession();
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());
           
        }

        private string ParsePath(string url) =>
            url.Split(new[] {'?', '#'}, StringSplitOptions.RemoveEmptyEntries)[0];
        
        private void ParseParameters()
        {
            if (!this.Url.Contains("?"))
            {
                return;
            }

           this.ParseQuery(this.Url, this.UrlParameters);
        }

        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                var currentLine = requestLines[i];
                var headerParts = currentLine.Split(new[] { ": " },
                    StringSplitOptions.RemoveEmptyEntries);

                if (headerParts.Length != 2)
                {
                    BadRequestException.ThrowFromInvalidRequest();
                }

                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim();
                var header = new HttpHeader(headerKey, headerValue); //check for repetitions?

                this.Headers.Add(header);
            }

            if (!this.Headers.ContainsKey("Host"))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsKey(HttpHeader.Cookie))
            {
                var allCookies = this.Headers.Get(HttpHeader.Cookie);

                foreach (var cookie in allCookies)
                {
                    if (!cookie.Value.Contains('='))
                    {
                        return;
                    }
                    var cookieParts = cookie
                        .Value
                        .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    if (cookieParts.Any())
                    {
                        continue;
                    }

                    foreach (var cookiePart in cookieParts)
                    {
                        var cookiekeyValuePair = cookiePart
                            .Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (cookiekeyValuePair.Length == 2)
                        {
                            var key = cookiekeyValuePair[0].Trim();
                            var value = cookiekeyValuePair[1].Trim();

                            this.Cookies.Add(new HttpCookie(key, value, false));
                        }
                    }
                }
            }
        }

        private HttpRequestMethod ParseMethod(string method)
        {
            HttpRequestMethod parsedMethod;
            if (!Enum.TryParse(method, true, out parsedMethod))
            {
                BadRequestException.ThrowFromInvalidRequest();
            }
       
            return parsedMethod;
        }

        private void ParseFormData(string formDataLine)
        {
            if (this.Method == HttpRequestMethod.Get)
            {
                return;
            }

            this.ParseQuery(formDataLine, this.QueryParameters);
        }

        private void ParseQuery(string queryString, IDictionary<string, string> dict)
        {
            var query = queryString
                .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();

            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dict.Add(queryKey, queryValue);
            }
        }

        public void SetSession()
        {
           if (this.Cookies.ContainsKey(SessionStore.SessionCookieKey))
            {
                var cookie = this.Cookies.Get(SessionStore.SessionCookieKey);
                var sessionId = cookie.Value;

                this.Session = SessionStore.Get(sessionId);
            }
        }

        //public override string ToString() => this.requestText;

    }
}
