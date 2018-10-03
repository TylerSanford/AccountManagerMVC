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
        //[Authorize(Roles = "Administrator")]
        public ActionResult Index(UniversalViewModel model) {
            //if (option == "Email") {
            //    return View(db.aspnet_Users.Where(x => x.aspnet_Membership.Email.StartsWith(search) || search == null).ToList());
            //} else if (option == "Role") {
            //    return View(Roles.GetUsersInRole(search).ToList());
            //} else {

            // Entity Framework Way
            //var aspnet_Users = db.aspnet_Users.Include(a => a.aspnet_Membership);
            // Membership provider - dont use in the end. Convert to array, convert to list, convert to list of entity framework entity users

            model.SearchString = model.SearchString.ToString("").ToLower();

            if (model.ClearSearch.IsNull()) {

                if (model.SearchOption == AM.Model.Enums.SearchOptions.None) {
                    model.aspnet_Users = Membership.GetAllUsers().ToList();
                } else if (model.SearchOption == AM.Model.Enums.SearchOptions.Email) {
                    //model.aspnet_Users = Membership.FindUsersByName(search).ToList();

                    model.aspnet_Users = db.aspnet_Users.Where(u => u.LoweredUserName.Contains(model.SearchString)).ToList().Select(u => Membership.GetUser(u.UserName)).ToList();
                    model.IsSuccess = true;
                } else if (model.SearchOption == AM.Model.Enums.SearchOptions.Role) {
                    try {
                        //model.aspnet_Users = Roles.GetUsersInRole(search).Select(u => Membership.GetUser(u)).ToList();

                        model.aspnet_Users = db.aspnet_Users.Where(u => u.aspnet_Roles.Any(r => r.LoweredRoleName.Contains(model.SearchString))).ToList().Select(u => Membership.GetUser(u.UserName)).ToList();

                        model.IsSuccess = true;

                    } catch (Exception ex) {
                        model.aspnet_Users = Membership.GetAllUsers().ToList();
                        model.IsSuccess = false;
                        model.StatusMsg = "Error: " + ex.Message;
                    }
                }
            } else {
                model.SearchString = "";
                model.aspnet_Users = Membership.GetAllUsers().ToList();

            }

            if (Request.IsAjaxRequest()) {
                return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Index", "Users", new { area = "", IsSuccess = model.IsSuccess, SearchString = model.SearchString, SearchOption = model.SearchOption, ViewMode = AM.Model.Enums.ViewModes.Search }) }, JsonRequestBehavior.AllowGet);
            }

            model.ViewingPage = AM.Model.Enums.ViewingPage.UserList;

            return View(model);

            // }
        }

        public ActionResult SearchPartial(UniversalViewModel model) {
            return PartialView(model);
        }

        // GET: User/Details/5
        //public ActionResult Details(Guid? guid) {
        //    UniversalViewModel model = new UniversalViewModel();
        //    if (guid == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    model.aspnet_User = Membership.GetUser(guid);
        //    ////aspnet_Users aspnet_Users = db.aspnet_Users.Find(id);
        //    if (model.aspnet_User == null) {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

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
        public ActionResult Create(UniversalViewModel model) {
            Context = new HttpContextWrapper(System.Web.HttpContext.Current);

            model.IsSuccess = ModelState.IsValid;
            model.StatusMsg = ModelState.ErrorMessages();

            if (model.IsSuccess)
                try {
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
                    } else {
                        model.Guid = memUser.ProviderUserKey.ToString().ToGuid();
                        model.StatusMsg = "Your account has successfully been created!";

                    }

                    if (model.ViewMode == Model.Enums.ViewModes.Register)
                        model.SelectedRoles.Add("User");

                    Roles.AddUserToRoles(model.Email, model.SelectedRoles.ToArray());
                } catch (Exception ex) {
                    model.IsSuccess = false;
                    model.StatusMsg = "Error: " + ex.Message;
                }

            model.ViewMode = Model.Enums.ViewModes.Create;

            if (Request.IsAjaxRequest()) {
                return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Index", "Users", new { area = "", IsSuccess = model.IsSuccess, ViewMode = AM.Model.Enums.ViewModes.Create }) }, JsonRequestBehavior.AllowGet);
            }

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



            model.Email = model.aspnet_User.Email;
            //model.CurrentEmail = model.Email;
            model.Guid = model.aspnet_User.ProviderUserKey.ToString().ToGuid();
            model.ViewMode = Model.Enums.ViewModes.Edit;
            model.UserRoles = Roles.GetAllRoles().ToList();
            model.UserRoleList = new SelectList(model.UserRoles);
            model.SelectedRoles = Roles.GetRolesForUser(model.aspnet_User.UserName).ToList();

            Profile profile = db.Profiles.FirstOrDefault(u => u.UserId == model.Guid);

            if (profile.IsNotNull()) {
                model.FirstName = profile.FirstName;
                model.LastName = profile.LastName;
                model.PhoneNumber = profile.PhoneNumber;
            }


            model.IsSuccess = true;

            if (Request.IsAjaxRequest()) {
                return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Edit", "Users", new { area = "", guid = model.Guid }) }, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(UniversalViewModel model) {
            model.IsSuccess = ModelState.IsValid;
            model.StatusMsg = ModelState.ErrorMessages();

            if (model.IsSuccess) {
                model.aspnet_User = Membership.GetUser(model.Guid);
                if (model.aspnet_User.IsNotNull()) {
                    // TODO: Need to update username using entity framework
                    AM.Model.aspnet_Users user = db.aspnet_Users.FirstOrDefault(au => au.UserId == model.Guid);

                    // Entity framework changes
                    user.UserName = model.Email.ToLower();
                    user.LoweredUserName = model.Email.ToLower();
                    user.aspnet_Membership.Email = model.Email.ToLower();
                    user.aspnet_Membership.LoweredEmail = model.Email.ToLower();

                    Profile profile = db.Profiles.FirstOrDefault(u => u.UserId == model.Guid);



                    if (profile.IsNull()) {
                        profile = new Profile() {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            PhoneNumber = model.PhoneNumber,
                            UserId = model.Guid,
                            Guid = Guid.NewGuid()
                        };

                        db.Profiles.Add(profile);

                    } else {
                        profile.FirstName = model.FirstName;
                        profile.LastName = model.LastName;
                        profile.PhoneNumber = model.PhoneNumber;
                    }

                    db.SaveChanges();

                    if (Roles.GetRolesForUser(user.UserName).IsNullOrEmpty()) {
                        model.CurrentRole = "";
                    } else {
                        model.CurrentRole = Roles.GetRolesForUser(user.UserName)[0];

                    }

                    try {
                        List<String> assignedRoles = Roles.GetRolesForUser(user.UserName).ToList();
                        List<String> rolesToRemove = assignedRoles.Where(r => !model.SelectedRoles.Contains(r)).ToList();
                        List<String> rolesToAdd = model.SelectedRoles.Where(r => !assignedRoles.Contains(r)).ToList();

                        if (rolesToAdd.IsNotNullOrEmpty()) {
                            Roles.AddUserToRoles(user.UserName, rolesToAdd.ToArray());
                        }

                        if (rolesToRemove.IsNotNullOrEmpty()) {
                            Roles.RemoveUserFromRoles(user.UserName, rolesToRemove.ToArray());

                        }

                        // Other way
                        //for (int i = 0; i < assignedRoles.Count(); i++) {
                        //    if (!model.SelectedRoles.Contains(assignedRoles[i])) {
                        //        Roles.RemoveUserFromRole(user.UserName, assignedRoles[i]);
                        //    }
                        //}
                    } catch (Exception ex) {
                        model.IsSuccess = false;
                        model.StatusMsg = "Error: " + ex.Message;
                    }


                    // TODO: Change role assignment from singular to plural. Add all new roles to list, and new model list to roles, then remove roles from the difference of whats currently there.
                    //if (model.SelectedRoles != model.CurrentRole) {
                    //    Roles.RemoveUserFromRole(user.UserName, model.CurrentRole);
                    //    Roles.AddUserToRole(user.UserName, model.SelectedRole);
                    //}

                    //return RedirectToAction("Index", new { IsSuccess = true, ViewMode = AM.Model.Enums.ViewModes.Edit });
                } else {
                    model.IsSuccess = false;
                    //model.StatusMsg = "User with email " + model.Email.ToLower() + ", no longer exists. Please return to the Users Listing.";
                    model.StatusMsg = string.Format("User with email {0}, no longer exists. Please return to the Users Listing.", model.Email.ToLower());
                }
            }

            model.UserRoles = Roles.GetAllRoles().ToList();
            model.UserRoleList = new SelectList(model.UserRoles);

            model.ViewMode = Model.Enums.ViewModes.Edit;

            if (Request.IsAjaxRequest()) {
                return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Index", "Users", new { area = "", IsSuccess = model.IsSuccess, ViewMode = AM.Model.Enums.ViewModes.Edit }) }, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

        // GET: User/Delete/5
        //public ActionResult Delete(Guid? guid) {
        //    UniversalViewModel model = new UniversalViewModel();
        //    if (guid == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    model.aspnet_User = Membership.GetUser(guid);
        //    if (model.aspnet_User == null) {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

        // POST: User/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(Guid? guid) {
            UniversalViewModel model = new UniversalViewModel();
            model.aspnet_User = Membership.GetUser(guid);

            Membership.DeleteUser(model.aspnet_User.ToString());
            //model.SaveChanges();
            model.IsSuccess = true;

            if (Request.IsAjaxRequest()) {
                return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Index", "Users", new { area = "", IsSuccess = model.IsSuccess, ViewMode = AM.Model.Enums.ViewModes.Delete }) }, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index", new { IsSuccess = true, ViewMode = AM.Model.Enums.ViewModes.Delete });
        }

        //[HttpPost]
        public ActionResult ChangePassword(UniversalViewModel model) {
            if (model.ViewingPage != AM.Model.Enums.ViewingPage.Home) {
                model.IsSuccess = true;
                model.StatusMsg = ModelState.ErrorMessages();

                if (model.IsSuccess) {
                    MembershipUser u = Membership.GetUser(model.Guid);
                    u.ChangePassword(u.ResetPassword(), model.Password);

                    model.Password = "";
                    model.ConfirmPassword = "";

                    if (Request.IsAjaxRequest()) {
                        return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg }, JsonRequestBehavior.AllowGet);
                    }
                }

            }

            return PartialView(model);
        }

        //// POST: User/Delete/5
        //[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid? guid) {
        //    UniversalViewModel model = new UniversalViewModel();
        //    model.aspnet_User = Membership.GetUser(guid);

        //    Membership.DeleteUser(model.aspnet_User.ToString());
        //    //model.SaveChanges();
        //    model.IsSuccess = true;

        //    if (Request.IsAjaxRequest()) {
        //        return Json(new { IsSuccess = model.IsSuccess, StatusMsg = model.StatusMsg, RedirectUrl = Url.Action("Index", "Users", new { area = "", IsSuccess = model.IsSuccess, ViewMode = AM.Model.Enums.ViewModes.Delete }) }, JsonRequestBehavior.AllowGet);
        //    }

        //    return RedirectToAction("Index", new { IsSuccess = true, ViewMode = AM.Model.Enums.ViewModes.Delete });
        //}

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
