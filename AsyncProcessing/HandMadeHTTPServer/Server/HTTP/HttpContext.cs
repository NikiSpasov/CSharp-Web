namespace _08.HandMadeHTTPServer.Server.HTTP
{
    using _08.HandMadeHTTPServer.Server.HTTP.Contracts;

    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(string requestStr)
        {
            this.request = new HttpRequest(requestStr);
        }

        public IHttpRequest Request => this.request;
    }
}
