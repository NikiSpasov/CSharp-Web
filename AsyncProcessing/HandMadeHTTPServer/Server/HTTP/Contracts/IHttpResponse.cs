namespace MyCoolWebServer.Server.HTTP.Contracts
{
    using Enums;
    using MyCoolWebServer.Server.Http.Contracts;

    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        IHttpHeaderCollection Headers { get; }

        IHttpCookieCollection Cookies { get; }
    }
}
