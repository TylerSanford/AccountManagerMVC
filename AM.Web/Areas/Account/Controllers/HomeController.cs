﻿using AM.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AM.Web.Areas.Account.Controllers {
    public class HomeController : BaseController {

        public ActionResult Index() {
            return RedirectToAction("index", "surgery", new { area = "" });
        }

    }
}