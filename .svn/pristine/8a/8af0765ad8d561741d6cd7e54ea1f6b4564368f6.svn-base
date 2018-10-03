using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AM.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Users_Manage_Details",
                url: "users/{guid}/details",
                defaults: new { area = "", controller = "Users", action = "details", guid = UrlParameter.Optional },
                namespaces: new string[] { "AM.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Users_Manage_Edit",
                url: "users/{guid}/edit",
                defaults: new { area = "", controller = "Users", action = "edit", guid = UrlParameter.Optional },
                namespaces: new string[] { "AM.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Users_Manage_Delete",
                url: "users/{guid}/delete",
                defaults: new { area = "", controller = "Users", action = "delete", guid = UrlParameter.Optional },
                namespaces: new string[] { "AM.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Users",
                url: "users/{action}",
                defaults: new { area = "", controller = "users", action = "index" },
                namespaces: new string[] { "AM.Web.Controllers" }
            );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { area = "", controller = "home", action = "index", id = UrlParameter.Optional },
               namespaces: new string[] { "AM.Web.Controllers" }
           );

        }
    }
}
