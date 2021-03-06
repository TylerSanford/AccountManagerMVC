﻿using AM.Model;
using PJX.Core.Enums;
using AM.Web.Controllers;
//using AM.Web.Mailers;
using AM.Web.Models;
using PJX.Core.Extensions;
using PJX.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;

namespace AM.Web.Areas.Account.Controllers {

    [Authorize]
    public class SignInController : BaseController {

        [AllowAnonymous]
        public ActionResult Index(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //[AllowAnonymous]
        //public ActionResult Force(Guid guid, string returnUrl) {
        //    CmiDbCtx = new Model.CMI_Entities();
        //    DbCtx = new PJX_Entities();
        //    ProjectShareRequest share = CmiDbCtx.ProjectShareRequests.First(psr => psr.Guid == guid);

        //    ViewBag.ReturnUrl = returnUrl;

        //    if (share.IsActive && returnUrl.Contains(share.Project.Guid.ToString())) {
        //        ViewBag.ShareRequest = share;
        //        AM.Web.Models.UniversalViewModel model = new AM.Web.Models.UniversalViewModel();

        //        return View("Index", model);
        //    }

        //    return RedirectToAction("Index", new { ReturnUrl = returnUrl });
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AM.Web.Models.UniversalViewModel model, string returnUrl) {
            if (Membership.ValidateUser(model.Email, model.Password)) {
                FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);

                if (Url.IsLocalUrl(returnUrl) && !string.IsNullOrWhiteSpace(returnUrl)) {
                    return RedirectToLocal(returnUrl);
                } else {
                    // for return url that is not local (external website)
                    // may add external redirector at some point (for tracking external exit points)
                    return RedirectToAction("index", "home", new { area = "" });
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The email or password provided is incorrect.");

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AuthCheck(AM.Web.Models.UniversalViewModel model, string returnUrl) {
            if (model.IsLogin) {
                if (ModelState.ContainsKey("Password"))
                    ModelState["Password"].Errors.Clear();

                if (ModelState.IsValid)
                    return Json(new { IsSuccess = true, StatusMessage = "" });
                else
                    return Json(new { IsSuccess = false, StatusMessage = string.Join(" ", ModelState.Errors()) });
            }

            if (model.IsAuthCheck) {
                if (model.Email.IsNullOrWhiteSpace())
                    return Json(new { IsSuccess = true, IsNotValidLogin = false, StatusMessage = "Your email or username is required." });

                // check if the username is a project login
                MembershipUser memUser = null;

                try {
                    memUser = Membership.GetUser(model.Email);
                } catch (Exception ex) {
                    return Json(new { IsSuccess = true, IsNotValidLogin = false, StatusMessage = "Your user could not be validated at this time. Please try again or let support know about it." });
                }

                if (memUser.IsNotNull())
                    if (memUser.ProviderUserKey.IsNotNull())
                        if (memUser.ProviderUserKey.ToString().ToGuid() != Guid.Empty)
                            if (memUser.Roles().Contains(UserRoles.Manager.ToString()))
                                return Json(new { IsSuccess = true, IsNotValidLogin = false, StatusMessage = "" });
            }

            return Json(new { IsSuccess = true, IsNotValidLogin = true, StatusMessage = "" });
        }
        
        public ActionResult Expire() {
            string previousUrl = Request.UrlReferrer.PathAndQuery;
            FormsAuthentication.SignOut();

            return RedirectToLocal(previousUrl);
        }

        //public ActionResult UserLoginActions(AM.Web.Models.UniversalViewModel model, string returnUrl) {
        //    // login is authenticated, check for dealer account creation rewards point
        //    // and add it to account if it hasn't been already
        //    PJX_Entities ctx = new PJX_Entities();
        //    CMI.Model.CMI_Entities cmiCtx = new CMI.Model.CMI_Entities();

        //    DbCtx = ctx;

        //    if (CurrentProfile.IsNull()) {
        //        // setup the default user record and address book record if they don't exist
        //        if (CurrentUser.IsNotNull()) {
        //            if (CurrentUser.ProviderUserKey.IsNotNull()) {
        //                PJX.Core.Model.User newUser = new PJX.Core.Model.User() {
        //                    Guid = CurrentUser.ProviderUserKey.ToString().ToGuid(),
        //                    FirstName = "",
        //                    LastName = "",
        //                    CompanyName = "",
        //                    TimeZoneOffset = TimeZoneOffset,
        //                    TimeZoneId = TimeZone.Id,
        //                    IsEmailOptIn = false,
        //                    IsActive = true
        //                };

        //                ctx.Users.Add(newUser);
        //                ctx.SaveChanges();
        //            }
        //        }
        //    }

        //    if (CurrentProfile.Address.IsNull()) {
        //        PJX.Core.Model.Address newAddr = new PJX.Core.Model.Address() {
        //            UserGuid = CurrentUser.ProviderUserKey.ToString().ToGuid(),
        //            UserId = CurrentProfile.UserId,
        //            Label = "Default",
        //            FirstName = CurrentProfile.FirstName,
        //            LastName = CurrentProfile.LastName,
        //            CompanyName = "",
        //            Address1 = "",
        //            Address2 = "",
        //            City = "",
        //            State = "",
        //            PostalCode = "",
        //            CountryCode = "US",
        //            Phone = "",
        //            Fax = "",
        //            Mobile = "",
        //            TollFree = "",
        //            Website = "",
        //            Email = CurrentUser.Email.ToLower(),
        //            IsDeleted = false
        //        };

        //        ctx.Addresses.Add(newAddr);
        //        ctx.SaveChanges();
        //    }

        //    // save project login user info (if needed)
        //    try {
        //        System.Web.HttpContext context = System.Web.HttpContext.Current;
        //        string ipAddress = context.Request.UserHostAddress.ToString();

        //        if (Request.IsLocal) ipAddress = "127.0.0.1";

        //        CMI.Model.ProjectUserLogin pul = new Model.ProjectUserLogin() {
        //            UserId = CurrentProfile.UserId,
        //            Name = model.Name,
        //            Company = model.Company,
        //            Email = model.Email,
        //            LoginDate = DateTime.UtcNow,
        //            IpAddress = ipAddress,
        //            Location = "" // todo: add IP localization
        //        };

        //        CmiDbCtx = cmiCtx;

        //        if (IsProjectLogin && Projects.Count == 1) pul.ProjectId = Project.ProjectId;

        //        cmiCtx.ProjectUserLogins.Add(pul);
        //        cmiCtx.SaveChanges();
        //    } catch (DbEntityValidationException ex) {
        //        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //        System.Web.HttpContext context = System.Web.HttpContext.Current;
        //        string ipAddress = context.Request.UserHostAddress.ToString();
        //        List<string> errors = ex.EntityValidationErrors.SelectMany(error => error.ValidationErrors).Select(e => e.ErrorMessage).ToList();

        //        logger.Fatal("Client IP: " + ipAddress);
        //        logger.Fatal(ex);
        //        logger.Fatal("DB Validation: " + string.Join(" ", errors));
        //    } catch (Exception ex) {
        //        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //        System.Web.HttpContext context = System.Web.HttpContext.Current;
        //        string ipAddress = context.Request.UserHostAddress.ToString();

        //        logger.Fatal("Client IP: " + ipAddress);
        //        logger.Fatal(ex);
        //    }

        //    if (!string.IsNullOrWhiteSpace(returnUrl))
        //        return Redirect(returnUrl);

        //    return RedirectToAction("index", "home", new { area = "projects" });
        //}

        //public ActionResult SendNewAccountWelcome(EmailNotificationUser u) {
        //    SmtpClient smtp = new SmtpClient();
        //    UserMailer mailer = new UserMailer();
        //    EmailContact contact = new EmailContact();

        //    //u = new EmailNotificationUser();
        //    //u.Email = "adam@projex.co";
        //    //u.User = new Core.Model.User() {
        //    //    FirstName = "Adam",
        //    //    LastName = "Bauerle"
        //    //};

        //    contact.To.Add(u.Email);
        //    contact.From = "noreply@constructionmanagementinc.com";

        //    var msg = mailer.NewAccountWelcome(contact, u.User, "");

        //    smtp.Send(msg); // send the notification

        //    return View();
        //}

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            } else {
                return RedirectToAction("index", "home", new { area = "" });
            }
        }

        public enum ManageMessageId {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }
        #endregion

    }
}