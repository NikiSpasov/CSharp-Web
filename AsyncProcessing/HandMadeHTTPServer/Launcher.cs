namespace MyCoolWebServer.Server
{
    using Contracts;
    using MyCoolWebServer.Application;
    using Routing;
    using Routing.Contracts;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new MainApplication();
            IAppRouteConfig routeConfig = new AppRouteConfig();
            mainApplication.Configure(routeConfig);
            this.webServer = new WebServer(1337, routeConfig);

            this.webServer.Run();
        }
    }
}
