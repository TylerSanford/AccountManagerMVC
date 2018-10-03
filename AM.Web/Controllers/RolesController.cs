using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AM.Model;
using AM.Web.Models;
using PJX.Core.Extensions;

namespace AM.Web.Controllers {
    public class RolesController : BaseController {
        private AM_Entities db = new AM_Entities();

        // GET: Role
        //[Authorize(Roles = "Administrator")]
        public ActionResult Index(UniversalViewModel model) {

            model.aspnet_Users = Membership.GetAllUsers().ToList();
            model.RolesList = Roles.GetAllRoles().ToList();
            return View(model);
            // }
        }

        // GET: /Roles/Create
        public ActionResult Create() {
            return View();
        }

        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                Roles.CreateRole(collection["RoleName"].ToString());
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        public ActionResult Delete(string RoleName) {
            try {
                Roles.DeleteRole(RoleName);
                return RedirectToAction("Index");
            } catch (Exception ex) {
                return View();
            }
        }

        // GET: /Roles/Edit/2
        public ActionResult Edit(string roleName) {
            UniversalViewModel model = new UniversalViewModel();


            var thisRole = db.aspnet_Roles.Where(r => r.RoleName == roleName).FirstOrDefault();
            model.RoleId = thisRole.RoleId.ToString();
            model.Role = thisRole.RoleName;



            return View(model);
        }

        // POST: /Roles/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UniversalViewModel model) {
            try {
                Guid roleId = model.RoleId.ToGuid();
                var thisRole = db.aspnet_Roles.Where(r => r.RoleId == roleId).FirstOrDefault();

                thisRole.RoleName = model.Role;

                db.SaveChanges();

                return RedirectToAction("Index");

            } catch (Exception e) {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName) {
            UniversalViewModel model = new UniversalViewModel();

            if (!string.IsNullOrWhiteSpace(UserName)) {
                //model.aspnet_User = Membership.GetUser(UserName);
                //model.RolesForThisUser = Roles.GetRolesForUser(UserName).ToList();
                model.Guid = (Guid)Membership.GetUser(UserName).ProviderUserKey;
                //model.SelectedRoles = Roles.GetRolesForUser(model.aspnet_User.UserName).ToList();
                //model.UserRoles = Roles.GetAllRoles().ToList();

                // Repopulate Dropdown Lists
                model.aspnet_Users = Membership.GetAllUsers().ToList();
                model.RolesList = Roles.GetAllRoles().ToList();
            }

            return RedirectToAction("edit", "users", new { area = "", guid = model.Guid });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName) {
            UniversalViewModel model = new UniversalViewModel {
                aspnet_User = Membership.GetUser(UserName)
            };

            Roles.AddUserToRole(UserName, RoleName);

            // Repopulate Dropdown Lists
            model.aspnet_Users = Membership.GetAllUsers().ToList();
            model.RolesList = Roles.GetAllRoles().ToList();

            return View("Index", model);
        }
    }
}