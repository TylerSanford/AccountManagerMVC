using System.Web.Mvc;

namespace AM.Web.Areas.Account {
    public class AccountAreaRegistration : AreaRegistration {
        public override string AreaName {
            get {
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                name: "Account_SignIn",
                url: "account/login",
                defaults: new { area = "account", controller = "signin", action = "index" },
                namespaces: new[] { "AM.Web.Areas.Account.Controllers" }
            );

            context.MapRoute(
                "Account_default",
                "account/{controller}/{action}/{id}",
                defaults: new { area = "account", controller = "home", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "AM.Web.Areas.Account.Controllers" }
            );
        }
    }
}