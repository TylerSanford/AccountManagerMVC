using System;
using PJX.Core.Enums;
using PJX.Core.Extensions;
using PJX.Core.Model;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Collections.Generic;

namespace AM.Web.Controllers {
    public class BaseController : Controller {

        public BaseController() {
        }

        public BaseController(HttpContext context) {
            Context = new HttpContextWrapper(context);
        }

        public HttpContextWrapper Context { get; set; }

        //public RCMF_Entities DbCtx { get; set; }

        public new RouteData RouteData {
            get {
                return Context.Request.RequestContext.RouteData;
            }
        }
        public string RouteArea {
            get {
                return RouteData.Values["area"].ToString();
            }
        }
        public string RouteController {
            get {
                return RouteData.GetRequiredString("controller");
            }
        }
        public string RouteAction {
            get {
                return RouteData.GetRequiredString("action");
            }
        }

        public string BingMap_ApiKey {
            get {

                if (ConfigurationManager.AppSettings.Get("BingMap.ApiKey").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("BingMap.ApiKey"));

                return "";
            }
        }

        public List<string> FileExtensionsBlackList {
            get {
                string extList = "";

                if (ConfigurationManager.AppSettings.Get("FileExtensions.BlackList").IsNotNull())
                    extList = Convert.ToString(ConfigurationManager.AppSettings.Get("FileExtensions.BlackList"));

                if (!string.IsNullOrWhiteSpace(extList))
                    return extList.Split(',').ToList();

                return new List<string>();
            }
        }

        public MembershipUser CurrentUser {
            get {
                return Membership.GetUser(true);
            }
        }

        //public User CurrentProfile {
        //    get {
        //        Guid userGuid = CurrentUser.ProviderUserKey.ToString().ToGuid();

        //        return DbCtx.Users.FirstOrDefault(u => u.UserGuid == userGuid);
        //    }
        //}

        public bool IsAdministrator {
            get {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                return context.User.IsInRole(UserRoles.Administrator.ToString());
            }
        }

        public bool IsDeveloper {
            get {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                return context.User.IsInRole(UserRoles.Developer.ToString());
            }
        }

        public bool IsInternal {
            get {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                return context.User.IsInRole(UserRoles.Internal.ToString());
            }
        }

        public bool IsCustomer {
            get {
                System.Web.HttpContext context = System.Web.HttpContext.Current;

                return context.User.IsInRole(UserRoles.Customer.ToString());
            }
        }

        public int PageSize {
            get {
                int pgSize = 0;

                if (ConfigurationManager.AppSettings.Get("PageSize").IsNotNull())
                    int.TryParse(ConfigurationManager.AppSettings.Get("PageSize"), out pgSize);

                return pgSize;
            }
        }

        public int CartCleanupDays {
            get {
                int days = 0;

                if (ConfigurationManager.AppSettings.Get("General.CartCleanupDays").IsNotNull())
                    int.TryParse(ConfigurationManager.AppSettings.Get("General.CartCleanupDays"), out days);

                return days;
            }
        }

        public string BaseUrl {
            get {
                if (ConfigurationManager.AppSettings.Get("MvcMailer.BaseURL").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("MvcMailer.BaseURL"));

                return "";
            }
        }

        public string FacebookUrl {
            get {
                if (ConfigurationManager.AppSettings.Get("Social.FacebookUrl").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Social.FacebookUrl"));

                return "";
            }
        }

        public string InstagramUrl {
            get {
                if (ConfigurationManager.AppSettings.Get("Social.InstagramUrl").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Social.InstagramUrl"));

                return "";
            }
        }

        public string YouTubeUrl {
            get {
                if (ConfigurationManager.AppSettings.Get("Social.YouTubeUrl").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Social.YouTubeUrl"));

                return "";
            }
        }

        //public int TimeZoneOffset {
        //    get {
        //        int offset = 0;

        //        if (CurrentUser.IsNull()) {
        //            if (ConfigurationManager.AppSettings.Get("DefaultUtcTimeZoneOffset").IsNotNull())
        //                int.TryParse(ConfigurationManager.AppSettings.Get("DefaultUtcTimeZoneOffset"), out offset);
        //        } else {
        //            Guid userGuid = CurrentUser.ProviderUserKey.ToString().ToGuid();
        //            LS.Core.Model.User user = DbCtx.Users.FirstOrDefault(u => u.UserGuid == userGuid);

        //            if (user.IsNotNull())
        //                offset = user.TimeZoneOffset;
        //            else
        //                if (ConfigurationManager.AppSettings.Get("DefaultUtcTimeZoneOffset").IsNotNull())
        //                    int.TryParse(ConfigurationManager.AppSettings.Get("DefaultUtcTimeZoneOffset"), out offset);
        //        }

        //        return offset;
        //    }
        //}

        //public TimeZoneInfo TimeZone {
        //    get {
        //        if (CurrentUser.IsNull())
        //            return TimeZoneInfo.GetSystemTimeZones().First(t => t.Id == "Mountain Standard Time");
        //        else {
        //            Guid userGuid = CurrentUser.ProviderUserKey.ToString().ToGuid();
        //            LS.Core.Model.User user = DbCtx.Users.FirstOrDefault(u => u.UserGuid == userGuid);

        //            if (user.IsNotNull())
        //                return TimeZoneInfo.GetSystemTimeZones().First(t => t.Id == user.TimeZoneId);
        //            else
        //                return TimeZoneInfo.GetSystemTimeZones().First(t => t.Id == "Mountain Standard Time");
        //        }
        //    }
        //}

        public string BingMapApiKey {
            get {

                if (ConfigurationManager.AppSettings.Get("BingMap.ApiKey").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("BingMap.ApiKey"));

                return "";
            }
        }

        public string MailChimpApiKey {
            get {

                if (ConfigurationManager.AppSettings.Get("MailChimp.API.Key").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("MailChimp.API.Key"));

                return "";
            }
        }

        public string ContactEmail {
            get {

                if (ConfigurationManager.AppSettings.Get("General.ContactEmail").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("General.ContactEmail"));

                return "";
            }
        }

        public bool EmailNotificationsAllowed {
            get {
                bool allow = false;

                if (ConfigurationManager.AppSettings.Get("EmailNotificationsAllowed").IsNotNull())
                    bool.TryParse(ConfigurationManager.AppSettings.Get("EmailNotificationsAllowed"), out allow);

                return allow;
            }
        }

        /// <summary>
        /// Expire duration in hours
        /// </summary>
        public int ResetRequestExpireDuration {
            get {
                int expireDuration = 0;

                if (ConfigurationManager.AppSettings.Get("ResetRequestExpireDuration").IsNotNull())
                    int.TryParse(ConfigurationManager.AppSettings.Get("ResetRequestExpireDuration"), out expireDuration);

                return expireDuration;
            }
        }

        public string ShopifyShopUrl {
            get {

                if (ConfigurationManager.AppSettings.Get("Shopify.ShopUrl").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Shopify.ShopUrl"));

                return "";
            }
        }

        public string ShopifyApiKey {
            get {

                if (ConfigurationManager.AppSettings.Get("Shopify.ApiKey").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Shopify.ApiKey"));

                return "";
            }
        }

        public string ShopifyPassword {
            get {

                if (ConfigurationManager.AppSettings.Get("Shopify.Password").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Shopify.Password"));

                return "";
            }
        }

        public string ShopifySharedSecret {
            get {

                if (ConfigurationManager.AppSettings.Get("Shopify.SharedSecret").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Shopify.SharedSecret"));

                return "";
            }
        }

        public string ShopifyAccessToken {
            get {

                if (ConfigurationManager.AppSettings.Get("Shopify.AccessToken").IsNotNull())
                    return Convert.ToString(ConfigurationManager.AppSettings.Get("Shopify.AccessToken"));

                return "";
            }
        }

    }
}