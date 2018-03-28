namespace MyCoolWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;
    using Enums;
    using Http;
    using Http.Contracts;

    public interface IHttpRequest
    {
        IDictionary<string, string> FormData { get;  }

        IHttpHeaderCollection Headers { get; }

        string Path { get; }

        IDictionary<string, string> QueryParameters { get; }

        HttpRequestMethod Method { get; }

        string Url { get; }

        HttpCookieCollection Cookies { get; }

        IDictionary<string, string> UrlParameters { get; }

        IHttpSession Session { get; set; }

        void AddUrlParameter(string key, string value);
    }
}
