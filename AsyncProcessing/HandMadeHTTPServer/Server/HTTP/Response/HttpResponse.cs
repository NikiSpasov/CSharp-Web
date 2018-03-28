namespace MyCoolWebServer.Server.HTTP.Response
{
    using System.Text;
    using Contracts;
    using Enums;
    using Http.Contracts;
    using MyCoolWebServer.Server.Http;

    public abstract class HttpResponse : IHttpResponse
    {

        protected HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
        }

        public IHttpHeaderCollection Headers { get; set; }

        public IHttpCookieCollection Cookies { get; set; }

        public HttpStatusCode StatusCode { get; protected set; }

        private string statusCodeMessage => this.StatusCode.ToString();

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();

            var statusCode = (int)this.StatusCode;
            response.AppendLine($"HTTP/1.1 {statusCode} {this.statusCodeMessage}");

            response.AppendLine(this.Headers.ToString());

            return response.ToString();
        }

        public void AddHeader(string location, string redirectUrl)
        {
            var httpHeader = new HttpHeader(location, redirectUrl);
            this.Headers.Add(httpHeader);
        }
    }
}
