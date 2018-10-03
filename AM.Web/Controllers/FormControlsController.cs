using AM.Web.Models;
using PJX.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AM.Web.Controllers {
    public class FormControlsController : BaseController {
        // GET: FormControls
        public ActionResult RolesSelect(UniversalViewModel model) {

            if (model.SelectedRoles.IsNull())
                model.SelectedRoles = new List<string>();
            model.UserRoles = Roles.GetAllRoles().ToList();
            return View(model);
        }
    }
}