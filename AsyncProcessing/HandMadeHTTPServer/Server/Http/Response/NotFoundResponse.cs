namespace MyCoolWebServer.Server.HTTP.Response
{
    using System.Runtime.CompilerServices;
    using MyCoolWebServer.Server.Enums;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
