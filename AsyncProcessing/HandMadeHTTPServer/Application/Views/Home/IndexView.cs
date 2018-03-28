namespace MyCoolWebServer.Server.Application.Views.Home
{
    using MyCoolWebServer.Server.Contracts;
    public class IndexView : IView
    {
        public string View() => "<h1>WELCOME!</h1>";
    }
}