using AM.Model;
using AM.Model.Enums;
//using AM.Web.Areas.Account.Models;
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

    //[Authorize]
    //public class ManageController : BaseController {

    //    public ActionResult Index() {
    //        SS_Entities ctx = new SS_Entities();
    //        PJX_Entities pjxCtx = new PJX.Core.Model.PJX_Entities();

    //        DbCtx = pjxCtx;

    //        if (CurrentProfile.IsAdministrator || CurrentProfile.IsDeveloper ||
    //            CurrentProfile.IsInternal) {
    //            return RedirectToAction("profiles", "manage", new { area = "account" });
    //        }

    //        return RedirectToAction("profile", "manage", new { area = "account" });
    //    }

    //    [AllowAnonymous]
    //    public ActionResult Register(AM.Web.Areas.Account.Models.RegisterViewModel model) {
    //        CMI_Entities ctx = new CMI_Entities();
    //        PJX_Entities pjxCtx = new PJX_Entities();

    //        // check for countries
    //        List<Country> countries = pjxCtx.Countries.OrderBy(c => c.Name).ToList();

    //        // move US up to the top
    //        int index = countries.FindIndex(c => c.UPS_CountryCode == "US");
    //        Country usa = countries[index];

    //        countries.RemoveAt(index);
    //        countries.Insert(0, usa);

    //        model.Countries = new SelectList(countries, "UPS_CountryCode", "Name");

    //        // build list of account types
    //        List<AccountType> acctTypes = new List<AccountType>();

    //        acctTypes.Add(new AccountType("Contractor", "Contractor"));
    //        acctTypes.Add(new AccountType("Owner", "Owner"));

    //        model.AccountTypes = acctTypes;

    //        return View(model);
    //    }

    //    [AllowAnonymous]
    //    public ActionResult Verify(AM.Web.Areas.Account.Models.RegisterViewModel model) {
    //        if (!ModelState.IsValid) {
    //            model.ValidationMessage = string.Join(" ", ModelState.Errors());
    //            model.IsSuccess = false;
    //        } else
    //            model.IsSuccess = true;

    //        PJX_Entities ctx = new PJX_Entities();

    //        DbCtx = ctx;

    //        if (model.IsSuccess) {
    //            // create new account
    //            PJX.Core.Model.User user = new PJX.Core.Model.User();
    //            bool isUserAlreadyExist = !Membership.GetUserNameByEmail(model.Email).IsNullOrEmpty();

    //            // attempt to save the new account
    //            try {
    //                // create the ASP.NET membership user
    //                string userCreateMessage = string.Empty;
    //                MembershipUser newUser = null;

    //                if (CreateMembershipUser(model.Email, model.Password, model.Email, model.AccountType, ref userCreateMessage, ref newUser)) {
    //                    if (newUser.IsNotNull()) {
    //                        // save the user record first
    //                        user.FirstName = model.FirstName;
    //                        user.LastName = model.LastName;
    //                        user.TimeZoneOffset = TimeZoneOffset;
    //                        user.TimeZoneId = TimeZone.Id;
    //                        user.IsEmailOptIn = false;
    //                        user.IsActive = true;

    //                        // link the 2 together (ASP.NET membership user -> CMI user)
    //                        user.Guid = newUser.ProviderUserKey.ToString().ToGuid();

    //                        ctx.Users.Add(user);
    //                        ctx.SaveChanges();

    //                        // save the address book address next
    //                        PJX.Core.Model.Address address = ctx.Addresses.FirstOrDefault(aba => aba.UserId == user.UserId && !aba.IsDeleted);

    //                        if (address.IsNull() || address.AddressId == 0)
    //                            address = new PJX.Core.Model.Address() {
    //                                UserId = user.UserId,
    //                                Label = "Default",
    //                                FirstName = model.FirstName,
    //                                LastName = model.LastName,
    //                                CompanyName = model.Company,
    //                                Address1 = model.Address1,
    //                                Address2 = model.Address2,
    //                                City = model.City,
    //                                State = model.State,
    //                                PostalCode = model.ZipCode,
    //                                CountryCode = model.Country,

    //                                Phone = model.Phone,

    //                                UserGuid = user.Guid,
    //                                IsDeleted = false
    //                            };

    //                        if (address.AddressId == 0) {
    //                            ctx.Addresses.Add(address);
    //                            ctx.SaveChanges();  // generate addressId
    //                        }

    //                        ctx.SaveChanges();

    //                        // send out email notifications as needed
    //                        // 1) New Account Notification
    //                        UserRoles acctType = (UserRoles)model.AccountType;
    //                        if (EmailNotificationsAllowed)
    //                            SendNewAccountNotification(user, acctType);
    //                    } else {
    //                        model.IsSuccess = false;
    //                        model.ValidationMessage = "Your account could not be created at this time. Please try again.";
    //                    }
    //                } else {
    //                    model.ValidationMessage = userCreateMessage;
    //                    model.IsSuccess = false;
    //                }
    //            } catch (DbEntityValidationException ex) {
    //                List<DbEntityValidationResult> errors = ex.EntityValidationErrors.ToList();
    //                string errorMsg = "";

    //                foreach (DbEntityValidationResult valResult in errors) {
    //                    foreach (DbValidationError error in valResult.ValidationErrors) {
    //                        errorMsg += error.ErrorMessage + " ";
    //                    }
    //                }

    //                model.ValidationMessage = errorMsg;
    //                model.IsSuccess = false;
    //            } catch {
    //                model.IsSuccess = false;
    //                model.ValidationMessage = "Your account could not be created at this time. Please try again.";
    //            }

    //            // see if we need to undo account creation
    //            if (!model.IsSuccess) {
    //                if (user.IsNotNull()) {

    //                    if (user.UserId > 0 && !isUserAlreadyExist) {

    //                        // delete the address book address (mark it) and user (delete it)
    //                        if (ctx.Addresses.Any(aba => aba.UserId == user.UserId)) {
    //                            foreach (PJX.Core.Model.Address address in ctx.Addresses.Where(aba => aba.UserId == user.UserId)) {
    //                                address.IsDeleted = true;
    //                            }

    //                            ctx.SaveChanges();
    //                        }

    //                        if (ctx.Users.Any(u => u.UserId == user.UserId)) {
    //                            ctx.Users.Remove(user);
    //                            ctx.SaveChanges();
    //                        }
    //                    }

    //                    // prep for asp.net user removal
    //                    string username = Membership.GetUserNameByEmail(model.Email);

    //                    // delete the asp.net user
    //                    if (Membership.GetUser(username).IsNotNull() && !isUserAlreadyExist)
    //                        Membership.DeleteUser(username, true);
    //                } else { // user account not created, need to delete the asp.net user if exists
    //                    // prep for asp.net user removal
    //                    string username = Membership.GetUserNameByEmail(model.Email);

    //                    // delete the asp.net user
    //                    if (Membership.GetUser(username).IsNotNull() && !isUserAlreadyExist)
    //                        Membership.DeleteUser(username, true);
    //                }

    //                if (string.IsNullOrWhiteSpace(model.ValidationMessage)) {
    //                    model.ValidationMessage = "Your account could not be created at this time. Please try again or let us know about it.";
    //                }
    //            }

    //            // auto-login new account (done in create membership user function)
    //        }

    //        // determine return url after registration / auto-login
    //        string returnUrl = "/projects/dashboard";

    //        // if is a dealer, then send user to the dealer application
    //        if (!string.IsNullOrWhiteSpace(model.Email)) {
    //            MembershipUser user = Membership.GetUser(model.Email);

    //            // sign in the user
    //            FormsAuthentication.SetAuthCookie(user.UserName, true);
    //        }

    //        if (model.ValidationMessage.IsNullOrEmpty()) model.ValidationMessage = string.Empty;

    //        if (!string.IsNullOrWhiteSpace(returnUrl))
    //            model.ReturnUrl = returnUrl;

    //        if (Request.IsAjaxRequest())
    //            return Json(new { Success = model.IsSuccess, Message = model.ValidationMessage, Url = (BaseUrl + model.ReturnUrl) });

    //        return Redirect(returnUrl);
    //    }

    //    [AllowAnonymous]
    //    public ActionResult Reset(string resetGuid) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        Guid reset = resetGuid.ToGuid();
    //        PasswordResetViewModel model = new PasswordResetViewModel();
    //        UserAccountResetRequest resetRequest = ctx.UserAccountResetRequests.FirstOrDefault(r => r.ResetGuid == reset);

    //        if (resetRequest.IsNotNull())
    //            if (resetRequest.ResetId > 0)
    //                if (resetRequest.IsValid) {
    //                    model.ResetGuid = resetRequest.ResetGuid;
    //                    model.UserId = resetRequest.UserId;
    //                    model.Username = resetRequest.User.Username;

    //                    return View(model);
    //                }

    //        return RedirectToAction("display", "manage", new { area = "account" });
    //    }

    //    [AllowAnonymous]
    //    public ActionResult ResetPassword(PasswordResetViewModel model) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        PJX.Core.Model.User user = new PJX.Core.Model.User();
    //        UserAccountResetRequest resetRequest = new UserAccountResetRequest();

    //        if (!ModelState.IsValid) {
    //            model.StatusMsg = string.Join(" ", ModelState.Errors());
    //            model.IsSuccess = false;
    //        } else {
    //            model.IsSuccess = true;
    //        }

    //        user = ctx.Users.SingleOrDefault(u => u.UserId == model.UserId &&
    //                                              u.ResetRequests.Any(r => r.ResetGuid == model.ResetGuid));

    //        resetRequest = ctx.UserAccountResetRequests.FirstOrDefault(r => r.UserId == model.UserId &&
    //                                                                        r.ResetGuid == model.ResetGuid);

    //        if (model.IsSuccess) {
    //            // attempt to save the new password to account

    //            // see if the user account exists first
    //            if (user.IsNotNull()) {
    //                if (user.UserId > 0) {
    //                    if (resetRequest.IsNotNull()) {
    //                        if (resetRequest.ResetId > 0) {
    //                            if (!resetRequest.IsExpired) {
    //                                try {
    //                                    MembershipUser memUser = Membership.GetUser(user.Guid);

    //                                    if (memUser.IsNotNull()) {
    //                                        // unlock the user as well (if needed)
    //                                        memUser.UnlockUser();

    //                                        string tempPassword = memUser.ResetPassword();

    //                                        if (memUser.ChangePassword(tempPassword, model.Password)) {
    //                                            // mark the reset request as used (invalid)
    //                                            resetRequest.IsValid = false;
    //                                            resetRequest.ResetDate = DateTime.UtcNow;

    //                                            ctx.SaveChanges();

    //                                            model.IsSuccess = true;
    //                                            model.StatusMsg = "Your password was reset successfully.";
    //                                        } else {
    //                                            model.IsSuccess = false;
    //                                            model.StatusMsg = "Your password could not be reset at this time. Please try again.";
    //                                        }
    //                                    } else {    // user account does not exist
    //                                        model.IsSuccess = false;
    //                                        model.StatusMsg = "We're sorry, but we can not find your account at this time. Please try again or let us know about it.";
    //                                    }
    //                                } catch (DbEntityValidationException ex) {
    //                                    List<DbEntityValidationResult> errors = ex.EntityValidationErrors.ToList();
    //                                    string errorMsg = "";

    //                                    foreach (DbEntityValidationResult valResult in errors) {
    //                                        foreach (DbValidationError error in valResult.ValidationErrors) {
    //                                            errorMsg += error.ErrorMessage + " ";
    //                                        }
    //                                    }

    //                                    model.StatusMsg = errorMsg;
    //                                    model.IsSuccess = false;
    //                                } catch (Exception ex) {
    //                                    model.IsSuccess = false;
    //                                    model.StatusMsg = "Your password could not be reset at this time. Please try again.";
    //                                }

    //                                model.Username = user.Username;
    //                            } else {    // reset request is expired (default to 24 hours after initial request)
    //                                model.IsSuccess = false;
    //                                model.StatusMsg = "We're sorry, but your reset request seems to have expired, as new password reset requests only last for " + ResetRequestExpireDuration + " hours.<br /><br />Please request a new password reset request and try again.";
    //                            }
    //                        } else {    // reset request does not exist
    //                            model.IsSuccess = false;
    //                            model.StatusMsg = "We're sorry, but we can not find your reset request at this time. Please try again or let us know about it.";
    //                        }
    //                    } else {    // reset request does not exist
    //                        model.IsSuccess = false;
    //                        model.StatusMsg = "We're sorry, but we can not find your reset request at this time. Please try again or let us know about it.";
    //                    }
    //                } else {    // user account does not exist
    //                    model.IsSuccess = false;
    //                    model.StatusMsg = "We're sorry, but we can not find your account at this time. Please try again or let us know about it.";
    //                }
    //            } else {        // user account does not exist
    //                model.IsSuccess = false;
    //                model.StatusMsg = "We're sorry, but we can not find your account at this time. Please try again or let us know about it.";
    //            }
    //        }

    //        if (user.IsNotNull())
    //            if (user.UserId > 0)
    //                model.Username = user.Username;

    //        // see if we need to sign in the user
    //        if (model.IsSuccess)
    //            if (!Request.IsAuthenticated)
    //                if (user.IsNotNull())
    //                    if (user.UserId > 0)
    //                        if (user.MembershipUser.IsApproved)
    //                            FormsAuthentication.SetAuthCookie(user.Username, true);

    //        // create the return result
    //        var result = new {
    //            Success = model.IsSuccess,
    //            Message = model.StatusMsg,
    //            NextUrl = (model.IsSuccess) ? "/account/manage" : ""
    //        };

    //        if (Request.IsAjaxRequest())
    //            return Json(result, JsonRequestBehavior.AllowGet);

    //        if (!model.IsSuccess)
    //            return View("reset", model);

    //        return RedirectToAction("index", "home", new { area = "" });
    //    }

    //    [AllowAnonymous]
    //    public ActionResult Status(Guid userGuid) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        PJX.Core.Model.User model = ctx.Users.FirstOrDefault(u => u.Guid == userGuid);

    //        return View(model);
    //    }

    //    /// <summary>
    //    /// Create new user for .NET membership
    //    /// </summary>
    //    /// <param name="username"></param>
    //    /// <param name="password"></param>
    //    /// <param name="email"></param>
    //    /// <param name="isCustomer"></param>
    //    /// <returns></returns>
    //    private bool CreateMembershipUser(string username, string password, string email, CMI.Model.Enums.UserRoles? type,
    //                                      ref string message, ref MembershipUser newUser) {
    //        UserRoles acctType = (UserRoles)type;
    //        MembershipCreateStatus status = new MembershipCreateStatus();
    //        newUser = Membership.CreateUser(username, password, email, "Question", "Answer", false, out status);
    //        bool success = false;

    //        switch (status) {
    //            case MembershipCreateStatus.Success:                // "Account Created"
    //                success = true;
    //                break;
    //            case MembershipCreateStatus.DuplicateUserName:      // "Username Already exists"
    //                message = "The email you entered is already registered.";
    //                break;
    //            case MembershipCreateStatus.DuplicateEmail:         // "Email already registered"
    //                message = "The email you entered is already registered.";
    //                break;
    //            case MembershipCreateStatus.InvalidEmail:           // "Invalid Email"
    //                message = "The email you entered is not a valid email address.";
    //                break;
    //            case MembershipCreateStatus.InvalidPassword:        // "Invalid password"
    //                message = "The password you entered is not a valid password.";
    //                break;
    //            default:                                            // "Error occured, account was not created"
    //                message = "Your user account could not be created at this time.";
    //                break;
    //        }

    //        if (!success) message += " Please try again.";

    //        if (success) {
    //            Roles.AddUserToRole(newUser.UserName, acctType.ToString());
    //            newUser.IsApproved = false;
    //        }

    //        return success;
    //    }


    //    public bool SendNewAccountNotification(PJX.Core.Model.User user, UserRoles role) {
    //        SmtpClient smtp = new SmtpClient();
    //        UserMailer mailer = new UserMailer();
    //        EmailContact contact = new EmailContact();

    //        contact.To.Add(NewAccountEmail);

    //        var msg = mailer.NewAccountNotify(contact, user, role, "/assets/img/email/features/air-suspension.jpg");

    //        smtp.Send(msg); // send the notification to the customer and Legend

    //        return true;
    //    }

    //    [Authorize(Roles = "Administrator, Developer, Internal, Manager")]
    //    public ActionResult Approve(Guid userGuid) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.Guid == userGuid);
    //        PJX.Core.Model.aspnetUser aspnetUser = ctx.aspnetUsers.FirstOrDefault(au => au.UserId == userGuid);

    //        DbCtx = ctx;

    //        if (user.IsNotNull())
    //            if (user.UserId > 0)
    //                if (aspnetUser.IsNotNull()) {
    //                    user.IsActive = true;
    //                    ctx.SaveChanges();

    //                    aspnetUser.aspnetMembership.IsApproved = true;
    //                    ctx.SaveChanges();

    //                    try {
    //                        user.MembershipUser.UnlockUser();
    //                        Membership.UpdateUser(user.MembershipUser);
    //                    } catch (Exception ex) { }
    //                }

    //        if (user.IsActive)
    //            if (user.MembershipUser.IsNotNull()) {
    //                SmtpClient smtp = new SmtpClient();
    //                UserMailer mailer = new UserMailer();
    //                EmailContact contact = new EmailContact();

    //                contact.To.Add(user.MembershipUser.Email);

    //                var msg = mailer.NewAccountApprovedNotify(contact, user, "/assets/img/email/features/air-suspension.jpg");

    //                smtp.Send(msg); // send the notification to the newly approved user
    //            }

    //        return View(user);
    //    }

    //    [Authorize(Roles = "Administrator, Developer, Internal, Manager")]
    //    public ActionResult Deny(Guid userGuid) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.Guid == userGuid);
    //        PJX.Core.Model.aspnetUser aspnetUser = ctx.aspnetUsers.FirstOrDefault(au => au.UserId == userGuid);

    //        DbCtx = ctx;

    //        if (user.IsNotNull())
    //            if (user.UserId > 0) {
    //                aspnetUser.aspnetMembership.IsApproved = false;
    //                ctx.SaveChanges();
    //            }

    //        return View(user);
    //    }

    //    [Authorize(Roles = "Administrator, Developer, Internal, Manager")]
    //    public ActionResult Profiles(ProfileManageViewModel model) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        model.Users = ctx.Users.ToList().Where(u => u.IsActive &&
    //                                                   !u.IsAdministrator &&
    //                                                   !u.IsInternal &&
    //                                                    u.Addresses != null)
    //                                        .OrderBy(u => u.MembershipUser.IsApproved)
    //                                        .ThenBy(u => u.LastName)
    //                                        .ThenBy(u => u.FirstName)
    //                                        .ToList();

    //        //DbCtx = pjxCtx;

    //        //model.PageId = Math.Max(model.PageId, 1) - 1;
    //        //model.PageSize = 50; // PageSize

    //        //model.Users = ctx.Users.Include("Addresses")
    //        //                       .Where(u => u.IsActive)
    //        //                       .OrderBy(u => u.LastName)
    //        //                       .ThenBy(u => u.FirstName)
    //        //                       .Skip(model.PageId * model.PageSize).Take(model.PageSize)
    //        //                       .ToList().Where(u => u.IsDealerStaff &&
    //        //                                           !u.IsAdministrator &&
    //        //                                           !u.IsInternal &&
    //        //                                            u.Addresses != null)
    //        //                       .OrderBy(u => u.MembershipUser.IsApproved)
    //        //                       .ThenBy(u => u.LastName)
    //        //                       .ThenBy(u => u.FirstName)
    //        //    //.Skip(model.PageId * model.PageSize).Take(model.PageSize)
    //        //                       .ToList();

    //        //model.UserCount = model.Users.Count;
    //        //model.PageCount = Math.Max(model.UserCount / model.PageSize, 1);

    //        //// check for last page
    //        //if (model.PageId > model.PageCount) {
    //        //    model.PageId = model.PageCount;
    //        //}

    //        return View(model);
    //    }

    //    public new ActionResult Profile(ProfileViewModel model) {
    //        PJX_Entities ctx = new PJX_Entities();

    //        DbCtx = ctx;

    //        model = new ProfileViewModel(CurrentProfile);

    //        if (model.Country.ToLower() == "usa" || string.IsNullOrWhiteSpace(model.Country))
    //            model.Country = "US";

    //        return View(model);
    //    }

    //    public ActionResult ProfileSave(ProfileViewModel model) {
    //        if (!ModelState.IsValid) {
    //            model.StatusMsg = string.Join(" ", ModelState.Errors());
    //            model.IsSuccess = false;
    //        } else
    //            model.IsSuccess = true;

    //        PJX_Entities ctx = new PJX_Entities();

    //        DbCtx = ctx;

    //        if (model.IsSuccess) {
    //            PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.UserId == CurrentProfile.UserId);

    //            if (user.IsNotNull()) {
    //                if (user.UserId > 0) {
    //                    // update user profile
    //                    user.FirstName = model.FirstName;
    //                    user.LastName = model.LastName;
    //                    user.IsEmailOptIn = model.IsEmailOptIn;
    //                    user.TimeZoneId = model.TimeZoneId;

    //                    // update user address book
    //                    PJX.Core.Model.Address userAddr = ctx.Addresses.FirstOrDefault(a => a.AddressId == CurrentProfile.Address.AddressId);

    //                    if (userAddr.IsNotNull()) {
    //                        if (userAddr.AddressId > 0) {
    //                            int countryId = (int)model.Country.ToInteger();

    //                            userAddr.FirstName = model.FirstName;
    //                            userAddr.LastName = model.LastName;

    //                            userAddr.CompanyName = model.Company;

    //                            userAddr.Address1 = model.Address1;
    //                            userAddr.Address2 = model.Address2;
    //                            userAddr.City = model.City;
    //                            userAddr.State = model.State;
    //                            userAddr.PostalCode = model.ZipCode;
    //                            userAddr.CountryCode = ctx.Countries.FirstOrDefault(c => c.CountryId == countryId).UPS_CountryCode;
    //                        }
    //                    }

    //                    // save to database
    //                    ctx.SaveChanges();

    //                    model.StatusMsg = "Your profile was updated successfully.";
    //                } else {
    //                    model.IsSuccess = false;
    //                    model.StatusMsg = "We are unable to update your profile at this time. Please try again later or let us know about it.";
    //                }
    //            } else {
    //                model.IsSuccess = false;
    //                model.StatusMsg = "We are unable to update your profile at this time. Please try again later or let us know about it.";
    //            }
    //        }

    //        if (Request.IsAjaxRequest())
    //            return Json(new {
    //                success = model.IsSuccess,
    //                Message = model.StatusMsg
    //            },
    //                        JsonRequestBehavior.AllowGet);

    //        model.Profile = CurrentProfile;

    //        return View("Profile", model);
    //    }

    //    public ActionResult Email(EmailViewModel model) {
    //        PJX_Entities ctx = new PJX_Entities();

    //        DbCtx = ctx;

    //        model.Email = CurrentUser.Email;

    //        return View(model);
    //    }

    //    public ActionResult EmailSave(EmailViewModel model) {
    //        if (!ModelState.IsValid) {
    //            model.StatusMsg = string.Join(" ", ModelState.Errors());
    //            model.IsSuccess = false;
    //        } else
    //            model.IsSuccess = true;

    //        PJX_Entities ctx = new PJX_Entities();

    //        DbCtx = ctx;

    //        if (model.IsSuccess) {
    //            if (!string.IsNullOrWhiteSpace(model.Email)) {
    //                // create password reset request and send out email notification for reset
    //                MembershipUser memUser = Membership.GetUser(Membership.GetUserNameByEmail(model.Email));

    //                if (memUser.IsNotNull()) {
    //                    Guid userGuid = memUser.ProviderUserKey.ToString().ToGuid();
    //                    PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.Guid == userGuid);

    //                    if (user.IsNotNull())
    //                        if (user.UserId > 0) {
    //                            UserAccountEmailChangeRequest changeRequest = new UserAccountEmailChangeRequest() {
    //                                UserId = user.UserId,
    //                                User = user,
    //                                ChangeRequestDate = DateTime.UtcNow,
    //                                ChangeGuid = Guid.NewGuid(),
    //                                IsValid = true
    //                            };

    //                            ctx.UserAccountEmailChangeRequests.Add(changeRequest);
    //                            ctx.SaveChanges();

    //                            // send out the email notification of the change request
    //                            SendEmailChangeRequestEmail(changeRequest);
    //                        }
    //                }
    //            }

    //            model.StatusMsg = "Your email change request has been received. Please check your email for email update instructions.";
    //        } else
    //            if (string.IsNullOrWhiteSpace(model.StatusMsg))
    //                model.StatusMsg = "Your email change request could not be processed at this time. Please try again or let us know about it.";

    //        if (Request.IsAjaxRequest())
    //            return Json(new {
    //                success = model.IsSuccess,
    //                Message = model.StatusMsg
    //            });

    //        DbCtx = ctx;

    //        return View(CurrentProfile);
    //    }

    //    public ActionResult EmailChangeRequest(string requestGuid) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        Guid reset = requestGuid.ToGuid();
    //        EmailViewModel model = new EmailViewModel();
    //        UserAccountEmailChangeRequest changeRequest = ctx.UserAccountEmailChangeRequests.FirstOrDefault(r => r.ChangeGuid == reset);

    //        if (changeRequest.IsNotNull())
    //            if (changeRequest.RequestId > 0)
    //                if (!changeRequest.IsExpired) {
    //                    model.RequestGuid = changeRequest.ChangeGuid;
    //                    model.UserId = changeRequest.UserId;
    //                    model.Username = changeRequest.User.Username;

    //                    return View(model);
    //                }

    //        return RedirectToAction("index", "home", new { area = "mylegend" });
    //    }

    //    public ActionResult EmailChangeProcess(EmailViewModel model) {
    //        PJX_Entities ctx = new PJX_Entities();
    //        PJX.Core.Model.User user = new PJX.Core.Model.User();
    //        UserAccountEmailChangeRequest changeRequest = new UserAccountEmailChangeRequest();

    //        if (!ModelState.IsValid) {
    //            model.StatusMsg = string.Join(" ", ModelState.Errors());
    //            model.IsSuccess = false;
    //        } else {
    //            model.IsSuccess = true;
    //        }

    //        user = ctx.Users.SingleOrDefault(u => u.UserId == model.UserId &&
    //                                              u.EmailChangeRequests.Any(r => r.ChangeGuid == model.RequestGuid));

    //        changeRequest = ctx.UserAccountEmailChangeRequests.FirstOrDefault(r => r.UserId == model.UserId &&
    //                                                                               r.ChangeGuid == model.RequestGuid);

    //        if (model.IsSuccess) {
    //            // attempt to save the new email to account

    //            // see if the user account exists first
    //            if (user.IsNotNull()) {
    //                if (user.UserId > 0) {
    //                    if (changeRequest.IsNotNull()) {
    //                        if (changeRequest.RequestId > 0) {
    //                            if (!changeRequest.IsExpired) {
    //                                try {
    //                                    PJX_Entities pjxCtx = new PJX_Entities();
    //                                    PJX.Core.Model.aspnetMembership mem = pjxCtx.aspnetMemberships.FirstOrDefault(m => m.UserId == user.Guid);
    //                                    MembershipUser memUser = Membership.GetUser(Membership.GetUserNameByEmail(mem.Email));

    //                                    if (memUser.IsNotNull()) {
    //                                        // make sure the new email is not already taken
    //                                        if (!pjxCtx.aspnetMemberships.Any(m => m.LoweredEmail == model.Email.ToLower())) {
    //                                            memUser.Email = model.Email;
    //                                            mem.aspnetUser.UserName = memUser.Email;
    //                                            mem.aspnetUser.LoweredUserName = memUser.Email.ToLower();

    //                                            // unlock the user as well (if needed)
    //                                            memUser.UnlockUser();

    //                                            // mark the change request as used (invalid)
    //                                            changeRequest.IsValid = false;
    //                                            changeRequest.ChangeDate = DateTime.UtcNow;

    //                                            Membership.UpdateUser(memUser);
    //                                            pjxCtx.SaveChanges();
    //                                            ctx.SaveChanges();

    //                                            model.IsSuccess = true;
    //                                            model.StatusMsg = "Your email was changed successfully.";
    //                                        } else {
    //                                            model.IsSuccess = false;
    //                                            model.StatusMsg = "We're sorry, but that email address is already used. Please try a different email address.";
    //                                        }
    //                                    } else {    // user account does not exist
    //                                        model.IsSuccess = false;
    //                                        model.StatusMsg = "We're sorry, but we can not find your account at this time. Please try again or let us know about it.";
    //                                    }
    //                                } catch (DbEntityValidationException ex) {
    //                                    List<DbEntityValidationResult> errors = ex.EntityValidationErrors.ToList();
    //                                    string errorMsg = "";

    //                                    foreach (DbEntityValidationResult valResult in errors) {
    //                                        foreach (DbValidationError error in valResult.ValidationErrors) {
    //                                            errorMsg += error.ErrorMessage + " ";
    //                                        }
    //                                    }

    //                                    model.StatusMsg = errorMsg;
    //                                    model.IsSuccess = false;
    //                                } catch (Exception ex) {
    //                                    model.IsSuccess = false;
    //                                    model.StatusMsg = "Your email could not be changed at this time. Please try again.";
    //                                }

    //                                model.Username = user.Username;
    //                            } else {    // reset request is expired (default to 24 hours after initial request)
    //                                model.IsSuccess = false;
    //                                model.StatusMsg = "We're sorry, but your change request seems to have expired, as new email change requests only last for " + ResetRequestExpireDuration + " hours.<br /><br />Please request a new email change request and try again.";
    //                            }
    //                        } else {    // reset request does not exist
    //                            model.IsSuccess = false;
    //                            model.StatusMsg = "We're sorry, but we can not find your change request at this time. Please try again or let us know about it.";
    //                        }
    //                    } else {    // reset request does not exist
    //                        model.IsSuccess = false;
    //                        model.StatusMsg = "We're sorry, but we can not find your change request at this time. Please try again or let us know about it.";
    //                    }
    //                } else {    // user account does not exist
    //                    model.IsSuccess = false;
    //                    model.StatusMsg = "We're sorry, but we can not find your change request at this time. Please try again or let us know about it.";
    //                }
    //            } else {        // user account does not exist
    //                model.IsSuccess = false;
    //                model.StatusMsg = "We're sorry, but we can not find your change request at this time. Please try again or let us know about it.";
    //            }
    //        }

    //        if (user.IsNotNull())
    //            if (user.UserId > 0)
    //                model.Username = user.Username;

    //        // see if we need to sign in the user
    //        if (model.IsSuccess)
    //            if (!Request.IsAuthenticated)
    //                if (user.IsNotNull())
    //                    if (user.UserId > 0)
    //                        if (user.MembershipUser.IsApproved)
    //                            FormsAuthentication.SetAuthCookie(user.Username, true);

    //        // create the return result
    //        var result = new {
    //            Success = model.IsSuccess,
    //            Message = model.StatusMsg,
    //            NextUrl = (model.IsSuccess) ? "/account/manage" : ""
    //        };

    //        if (Request.IsAjaxRequest())
    //            return Json(result, JsonRequestBehavior.AllowGet);

    //        if (!model.IsSuccess)
    //            return View("reset", model);

    //        return RedirectToAction("index", "home", new { area = "" });
    //    }

    //    public bool SendEmailChangeRequestEmail(UserAccountEmailChangeRequest changeRequest) {
    //        var contact = new EmailContact();
    //        MembershipUser memUser = Membership.GetUser(changeRequest.User.Guid);

    //        contact.From = "noreply@constructionmanagementinc.com";
    //        contact.To.Add(memUser.Email);

    //        SmtpClient smtp = new SmtpClient();
    //        var mailer = new UserMailer();
    //        var msg = mailer.EmailChangeRequest(contact, changeRequest, "/assets/img/email/features/air-suspension.jpg");

    //        if (EmailNotificationsAllowed) smtp.Send(msg);

    //        return true;
    //    }
    //}

    //public class AccountType {

    //    public string Value { get; set; }
    //    public string Name { get; set; }

    //    public AccountType() {
    //    }

    //    public AccountType(string value, string name) {
    //        Value = value;
    //        Name = name;
    //    }

    //}

}