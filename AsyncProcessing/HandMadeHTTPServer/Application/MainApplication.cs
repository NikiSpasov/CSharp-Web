namespace MyCoolWebServer.Application
{
    using MyCoolWebServer.Server.Application.Controllers;
    using MyCoolWebServer.Server.Contracts;
    using MyCoolWebServer.Server.Handlers;
    using MyCoolWebServer.Server.Routing.Contracts;

    public class MainApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.AddRoute("/", new GetHandler(request => new HomeController().Index()));

            appRouteConfig.AddRoute(
                "/users/{(?<name>[a-z]+)}", 
                new GetHandler(request => new HomeController().Index())
                );

            appRouteConfig.AddRoute("/testsession", 
                new GetHandler(request => new HomeController().SessionTest(request)));
        }
    }
}
