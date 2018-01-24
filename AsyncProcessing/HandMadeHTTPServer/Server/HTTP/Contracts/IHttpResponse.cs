namespace _08.HandMadeHTTPServer.Server.HTTP.Contracts
{
    public interface IHttpResponse
    {
        string Response { get; }

        void AddHeader(string location, string redirectUrl);
    }
}
