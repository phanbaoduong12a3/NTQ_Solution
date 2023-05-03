using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NTQ_Solution
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Logout",
              url: "Logout/Index",
              defaults: new { controller = "Login", action = "Logout", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
             );

            routes.MapRoute(
              name: "Login",
              url: "Login/Index",
              defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
             );

            routes.MapRoute(
              name: "Add Cart",
              url: "add-Order",
              defaults: new { controller = "Home", action = "Order", id = UrlParameter.Optional },
             namespaces: new[] { "OnlineShop.Controllers" }
             );
            routes.MapRoute(
              name: "Get Product Of Supplier",
              url: "GetProductOfSupplier",
              defaults: new { controller = "Supplier", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "NTQ_Solution.Areas.Admin.Controllers" }
             );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{slug}-{id}",
                defaults: new {controller = "Home",action = "Detail", id = UrlParameter.Optional},
                namespaces: new[] { "NTQ_Solution.Controllers" }
                ) ;

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NTQ_Solution.Controllers" }
            );
        }
    }
}
