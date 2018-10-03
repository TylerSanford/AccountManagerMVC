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
            SelectedRoles = new List<string>();
        }

        public Model.Enums.ViewModes ViewMode { get; set; }
        public ViewingPage ViewingPage { get; set; }

        public Guid Guid { get; set; }
        public List<MembershipUser> aspnet_Users { get; set; }
        public MembershipUser aspnet_User { get; set; }
        public int UserCount { get; set; }
        public int UsersOnline { get; set; }
        public List<String> RolesList { set; get; }

        #region Login Properties

        public String ReturnUrl { get; set; }
        public bool RememberMe { get; set; }

        public bool IsLogin {
            get {
                return ViewingPage == ViewingPage.Login;
            }
        }
        public bool IsAuthCheck { get; set; }

        #endregion

        #region User Profile

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        #endregion

        #region User Shared Properties

        [RequiredIfTrue("IsPasswordRequired", ErrorMessage = "{0} is required.")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "{0} Must contain: 1 Uppercase, 1 LowerCase, 1 Digit, 1 Special Character, Minimum of 8 Digits")]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [RequiredIfTrue("IsComfirmPasswordRequired", ErrorMessage = "{0} is required.")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        //[RequiredIfFalse("IsConfirmPasswordComparisonNotRequired", ErrorMessage = "Password and Confirm Password do not match.")]
        [Display(Name = "Confirm Password")]
        public String ConfirmPassword { get; set; }

        public bool IsPasswordRequired {
            get {
                return IsLogin || IsUserRegister || IsUserCreate;
            }
        }

        public bool IsComfirmPasswordRequired {
            get {
                return IsUserRegister || IsUserCreate;
            }
        }

        public bool IsConfirmPasswordComparisonNotRequired {
            get {
                return !IsComfirmPasswordRequired;
            }
        }

        public bool IsPasswordRegexMatch {
            get {
                return IsComfirmPasswordRequired;
            }
        }

        #endregion


        #region Role Properties

        public String Role { get; set; }
        public String RoleId { get; set; }

        #endregion


        #region User / Login Properties

        [RequiredIfTrue("IsEmailRequired", ErrorMessage = "{0} is required.")]
        [Display(Name = "Email")]
        public String Email { get; set; }

        public bool IsEmailRequired {
            get {
                return IsLogin || IsUserEdit || IsUserRegister;
            }
        }

        #endregion

        #region User Properties

        [Display(Name = "Role")]
        public List<String> SelectedRoles { get; set; }
        public String CurrentRole { get; set; }
        public SelectList UserRoleList { get; set; }
        public List<String> UserRoles { get; set; }
        public List<String> RolesForThisUser { get; set; }

        public bool IsUserEdit {
            get {
                return ViewingPage == ViewingPage.UserEdit;
            }
        }

        public bool IsUserChangePassword {
            get {
                return ViewingPage == ViewingPage.UserChangePassword;
            }
        }

        public bool IsUserCreate {
            get {
                return ViewingPage == ViewingPage.UserCreate;
            }
        }

        #endregion


        #region Register Properties

        public bool IsUserRegister {
            get {
                return ViewingPage == ViewingPage.UserRegister;
            }
        }

        #endregion

        #region Search Details

        public SearchOptions SearchOption { get; set; }
        public string SearchString { get; set; }
        public string ClearSearch { get; set; }

        #endregion

        public bool IsSuccess { get; set; }
        public String StatusMsg { get; set; }
    }




}