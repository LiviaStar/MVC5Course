using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axpx/{*pathInfo}"); //Home/Webform1.aspx 加這段就可以跑

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}", //{*id} id後的/全部當成參數(不包含?後)
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
