namespace _08.HandMadeHTTPServer.Server.HTTP.Response
{
    using System.Net;
    using System.Text;
    using _08.HandMadeHTTPServer.Server.HTTP.Contracts;

    public abstract class HttpResponse : IHttpResponse
    {
        private readonly IView view;

        protected HttpResponse(string redirectUrl)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader("Location", redirectUrl);
        }

        protected HttpResponse(HttpStatusCode responseCode, IView view)
        {
            this.HeaderCollection = new HttpHeaderCollection();
            this.view = view;
            this.StatusCode = responseCode;
        }

        private HttpHeaderCollection HeaderCollection { get; set; }

        private HttpStatusCode StatusCode { get; set; }

        private string StatusMessage => this.StatusCode.ToString();

        public void AddHeader(string location, string redirectUrl)
        {
            var httpHeader = new HttpHeader(location, redirectUrl);
            this.HeaderCollection.Add(httpHeader);
        }

        public string Response
        {
            get
            {
                StringBuilder response = new StringBuilder();
                response.AppendLine($"HTTP/1.1 {(int) this.StatusCode} {this.StatusMessage}");
                response.AppendLine($"Content-type: text/html"); //????

                response.AppendLine();

                if ((int)this.StatusCode < 300 || (int)this.StatusCode > 400)
                {
                    response.AppendLine("this is not redirection status!"); //????
                }

                return response.ToString();

            }
            
        }
    }
}
