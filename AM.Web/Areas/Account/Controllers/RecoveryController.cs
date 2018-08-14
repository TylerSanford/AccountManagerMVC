using AM.Web.Areas.Account.Models;
using AM.Web.Controllers;
//using AM.Web.Mailers;
using AM.Web.Models;
using PJX.Core.Extensions;
using PJX.Core.Model;
using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;

//namespace AM.Web.Areas.Account.Controllers {
//    public class RecoveryController : BaseController {

//        public ActionResult Index(string username) {
//            RecoveryViewModel model = new RecoveryViewModel();

//            model.Username = username;

//            return View(model);
//        }

//        public ActionResult ResetPassword(RecoveryViewModel model) {
//            PJX_Entities ctx = new PJX_Entities();

//            if (!string.IsNullOrWhiteSpace(model.Username)) {
//                // create password reset request and send out email notification for reset
//                MembershipUser memUser = Membership.GetUser(model.Username);

//                if (memUser.IsNull()) {
//                    string username = Membership.GetUserNameByEmail(model.Username);

//                    if (!string.IsNullOrWhiteSpace(username))
//                        memUser = Membership.GetUser(username);

//                    if (memUser.IsNotNull()) {
//                        PJX_Entities pjxCtx = new PJX_Entities();
//                        aspnetUser aspnetUser = pjxCtx.aspnetUsers.FirstOrDefault(anu => anu.UserName == username);

//                        if (aspnetUser.IsNotNull()) {
//                            aspnetUser.UserName = model.Username;
//                            aspnetUser.LoweredUserName = model.Username.ToLower();

//                            pjxCtx.SaveChanges();
//                        }
//                    }
//                }

//                if (memUser.IsNotNull()) {
//                    Guid userGuid = memUser.ProviderUserKey.ToString().ToGuid();
//                    PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.Guid == userGuid);

//                    if (user.IsNotNull())
//                        if (user.UserId > 0) {
//                            UserAccountResetRequest resetRequest = new UserAccountResetRequest() {
//                                UserId = user.UserId,
//                                User = user,
//                                ResetRequestDate = DateTime.UtcNow,
//                                ResetGuid = Guid.NewGuid(),
//                                IsValid = true
//                            };

//                            ctx.UserAccountResetRequests.Add(resetRequest);
//                            ctx.SaveChanges();

//                            // send out the email notification of the reset request
//                            SendPasswordResetEmail(resetRequest);

//                            return RedirectToAction("resetsuccess", "recovery", new { area = "account", id = user.UserId });
//                        }
//                }
//            }

//            return RedirectToAction("resetsuccess", "recovery", new { area = "account", id = 0 });
//        }

//        public ActionResult ResetSuccess(int id) {
//            PJX_Entities ctx = new PJX_Entities();
//            PJX.Core.Model.User user = ctx.Users.FirstOrDefault(u => u.UserId == id);
//            MembershipUser model = Membership.GetUser((user.IsNotNull() ? user.Guid : Guid.Empty));

//            return View(model);
//        }

//        public bool SendPasswordResetEmail(UserAccountResetRequest resetRequest) {
//            var contact = new EmailContact();
//            MembershipUser memUser = Membership.GetUser(resetRequest.User.Guid);

//            contact.From = "noreply@constructionmanagementinc.com";
//            contact.To.Add(memUser.Email);

//            SmtpClient smtp = new SmtpClient();
//            var mailer = new UserMailer();
//            var msg = mailer.PasswordResetRequest(contact, resetRequest, "/assets/img/email/features/air-suspension.jpg");

//            if (EmailNotificationsAllowed) smtp.Send(msg);

//            return true;
//        }

//    }
//}