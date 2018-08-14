using Foolproof;
using PJX.Core.Enums;
using AM.Model;
using AM.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace AM.Web.Models {
    public class UniversalViewModel {

        public UniversalViewModel() {
        }

        public Model.Enums.ViewModes ViewMode { get; set; }

        public Guid Guid { get; set; }
        public MembershipUserCollection aspnet_Users { get; set; }
        public MembershipUser aspnet_User { get; set; }

        [Display(Name = "Role")]
        public string SelectedRole { get; set; }
        public string CurrentRole { get; set; }
        public SelectList UserRoleList { get; set; }
        public List<string> UserRoles { get; set; }

        #region Login Details

        public string ReturnUrl { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public bool IsLogin { get; set; }
        public bool IsAuthCheck { get; set; }

        #endregion


        #region Register Details

        // Email already included in Login Details Region
        // Password already included in Login Details Region
        public string ConfirmPassword { get; set; }


        #endregion

        public bool IsSuccess { get; set; }
        public string StatusMsg { get; set; }
    }
}