using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", // cats/neshto/drugo/150
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );

            //routes.MapRoute(
            //    name: "Default3",
            //    url: "cats/{controller}", // cats/neshto/drugo/150
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Default1",
            //    url: "dogs/{id}", // dogs/150
            //    defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            //    );

            //routes.MapRoute(
            //    name: "Default2",
            //    url: "{controller}/{action}/{id}", // cats/drugo/150
            //    defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            //    );
        }
    }
}
