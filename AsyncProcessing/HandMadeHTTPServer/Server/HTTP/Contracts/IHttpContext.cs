namespace MyCoolWebServer.Server.HTTP.Contracts
{
    using System.Collections.Generic;
    using System.Net;

    public interface IHttpContext
    {
        IHttpRequest Request { get; }

    }
}
