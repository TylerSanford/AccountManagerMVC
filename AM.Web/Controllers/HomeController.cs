using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AM.Web.Models;

namespace AM.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            UniversalViewModel model = new UniversalViewModel();

            model.UserCount = Membership.GetAllUsers().Count;
            model.UsersOnline = Membership.GetNumberOfUsersOnline();

            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}