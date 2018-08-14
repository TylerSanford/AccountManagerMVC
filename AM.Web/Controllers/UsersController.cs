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
    public class UsersController : BaseController {
        private AM_Entities db = new AM_Entities();

        // GET: User
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(UniversalViewModel model) {
            // Entity Framework Way
            //var aspnet_Users = db.aspnet_Users.Include(a => a.aspnet_Membership);
            model.aspnet_Users = Membership.GetAllUsers();
            return View(model);
        }

        // GET: User/Details/5
        public ActionResult Details(Guid? guid) {
            UniversalViewModel model = new UniversalViewModel();
            if (guid == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.aspnet_User = Membership.GetUser(guid);
            ////aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
            if (model.aspnet_User == null) {
                return HttpNotFound();
            }
            return View(model);
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string returnUrl) {
            UniversalViewModel model = new UniversalViewModel();

            Context = new HttpContextWrapper(System.Web.HttpContext.Current);

            model.ViewMode = Model.Enums.ViewModes.Register;

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        // GET: User/Create
        public ActionResult Create(string returnUrl) {
            UniversalViewModel model = new UniversalViewModel();

            Context = new HttpContextWrapper(System.Web.HttpContext.Current);

            model.ViewMode = Model.Enums.ViewModes.Create;

            model.ReturnUrl = returnUrl;

            return View(model);
        }

        /// <summary>
        /// Get Partial View of Create/Register Form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult CreateForm(UniversalViewModel model) {
            AM_Entities ctx = new AM_Entities();
            model.UserRoles = Roles.GetAllRoles().ToList();

            model.UserRoleList = new SelectList(model.UserRoles);

            return PartialView(model);
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// [HttpPost]: User/Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UniversalViewModel model) {
            Context = new HttpContextWrapper(System.Web.HttpContext.Current);



            model.IsSuccess = true;
            model.StatusMsg = "Your account has successfully been created!";



            if (ModelState.IsValid) {
                model.Email = model.Email.ToLower();


                MembershipCreateStatus status;
                MembershipUser memUser = Membership.CreateUser(model.Email, model.Password, model.Email, "Question", "Answer", true, out status);

                if (status != MembershipCreateStatus.Success) {
                    model.IsSuccess = false;
                    model.StatusMsg = "Failed";
                    switch (status) {
                        // Add More Cases**
                        case MembershipCreateStatus.DuplicateEmail:
                        case MembershipCreateStatus.DuplicateUserName:
                            model.StatusMsg = "Email already exists";
                            break;
                        case MembershipCreateStatus.InvalidPassword:
                            model.StatusMsg = "Password is not formatted correctly";
                            break;
                    }
                } else
                    model.Guid = memUser.ProviderUserKey.ToString().ToGuid();

                if (model.ViewMode == Model.Enums.ViewModes.Register) model.SelectedRole = "User";

                Roles.AddUserToRole(model.Email, model.SelectedRole);
            }

            //ViewBag.UserId = new SelectList(db.aspnet_Membership, "UserId", "Password", model.UserId);
            if (model.IsSuccess)
                return RedirectToAction("index", "users", new { area = "", isSuccess = true });

            return View(RouteAction, model);
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid? guid) {
            UniversalViewModel model = new UniversalViewModel();
            if (guid == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.aspnet_User = Membership.GetUser(guid);
            if (model.aspnet_User == null) {
                return HttpNotFound();
            }

            model.ViewMode = Model.Enums.ViewModes.Edit;

            model.UserRoles = Roles.GetAllRoles().ToList();
            model.UserRoleList = new SelectList(model.UserRoles);
            model.SelectedRole = Roles.GetRolesForUser(model.aspnet_User.UserName)[0];

            return View(model);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UniversalViewModel model) {
            model.IsSuccess = ModelState.IsValid;
            model.StatusMsg = ModelState.ErrorMessages();

            if (!model.IsSuccess) {
                model.aspnet_User = Membership.GetUser(model.Guid);
                if (model.aspnet_User.IsNotNull()) {
                    // TODO: Need to update username using entity framework
                    AM.Model.aspnet_Users user = db.aspnet_Users.FirstOrDefault(au => au.UserId == model.Guid);

                    user.UserName = model.Email.ToLower();
                    user.LoweredUserName = model.Email.ToLower();
                    user.aspnet_Membership.Email = model.Email.ToLower();
                    user.aspnet_Membership.LoweredEmail = model.Email.ToLower();
                    model.CurrentRole = Roles.GetRolesForUser(user.UserName)[0];

                    if (model.SelectedRole != model.CurrentRole) {
                        Roles.RemoveUserFromRole(user.UserName, model.CurrentRole);
                        Roles.AddUserToRole(user.UserName, model.SelectedRole);
                    }
                    //Membership.UpdateUser(model.aspnet_User);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                } else {
                    model.IsSuccess = false;
                    //model.StatusMsg = "User with email " + model.Email.ToLower() + ", no longer exists. Please return to the Users Listing.";
                    model.StatusMsg = string.Format("User with email {0}, no longer exists. Please return to the Users Listing.", model.Email.ToLower());
                }
            }

            model.ViewMode = Model.Enums.ViewModes.Edit;

            return View(model);
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid? guid) {
            UniversalViewModel model = new UniversalViewModel();
            if (guid == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.aspnet_User = Membership.GetUser(guid);
            if (model.aspnet_User == null) {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? guid) {
            UniversalViewModel model = new UniversalViewModel();
            model.aspnet_User = Membership.GetUser(guid);

            Membership.DeleteUser(model.aspnet_User.ToString());
            //model.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
