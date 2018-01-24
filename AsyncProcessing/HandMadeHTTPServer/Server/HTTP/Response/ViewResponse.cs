namespace _08.HandMadeHTTPServer.Server.HTTP.Response
{
    using System.Net;
    using _08.HandMadeHTTPServer.Server.HTTP.Contracts;

    public class ViewResponse : HttpResponse
    {
        public ViewResponse(HttpStatusCode responseCode, IView view) 
        : base(responseCode, view) { }
    }
}
