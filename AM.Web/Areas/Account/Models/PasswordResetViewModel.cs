using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace AM.Web.Areas.Account.Models {
    public class PasswordResetViewModel {

        private const string passRegEx = @"(?=^.{8,15}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$";

        public Guid ResetGuid { get; set; }

        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(passRegEx, ErrorMessage = "Password must be between 8 and 15 characters long, with at least 1 uppercase letter, 1 number and 1 special character.")]
        public string Password { get; set; }

        [EqualTo("Password", ErrorMessage = "Password and confirmation password fields must match.")]
        [Required(ErrorMessage = "Password confirmation is required.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
        public string StatusMsg { get; set; }

    }
}