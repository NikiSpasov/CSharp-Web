namespace MyCoolWebServer.Server.Application.Controllers
{
    using System;
    using MyCoolWebServer.Application.Views.Home;
    using MyCoolWebServer.Server.Application.Views.Home;
    using MyCoolWebServer.Server.Enums;
    using MyCoolWebServer.Server.Http;
    using MyCoolWebServer.Server.HTTP.Contracts;
    using MyCoolWebServer.Server.HTTP.Response;

    public class HomeController
    {
        // GET /
        public IHttpResponse Index()
        {
            var response =  new ViewResponse(HttpStatusCode.Ok, new IndexView());

            response.Cookies.Add(new HttpCookie("lang", "en"));

            return response;
        }
        // GET /testsession
        public IHttpResponse SessionTest(IHttpRequest req)
        {
            var session = req.Session;

            const string sessionDateKey = "saved_date";

            if (session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);

            }

            return new ViewResponse(
                HttpStatusCode.Ok,
                new SessionTestView(session.Get<DateTime>(sessionDateKey)));
          }
    }
}