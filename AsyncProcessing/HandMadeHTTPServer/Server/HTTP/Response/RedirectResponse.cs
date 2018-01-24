namespace _08.HandMadeHTTPServer.Server.HTTP.Response
{
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string redirectUrl) 
        : base(redirectUrl) { }
    }
}
