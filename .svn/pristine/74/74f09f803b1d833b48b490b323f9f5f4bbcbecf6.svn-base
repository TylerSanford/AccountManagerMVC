using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace AM.Web.Areas.Account.Models {
    public class PasswordViewModel {

        private const string passRegEx = @"^(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{8,12}$";

        public Guid Guid { get; set; }
        public int UserId { get; set; }
        public long Ticks { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }

        public bool IsExistingAccount { get; set; }

        [RequiredIfTrue("IsExistingAccount", ErrorMessage = "Old password is required.")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(passRegEx, ErrorMessage = "Password must be between 8 and 12 characters long, with at least 1 uppercase letter and 1 number.")]
        public string Password { get; set; }

        [EqualTo("Password", ErrorMessage = "Password and confirmation password fields must match.")]
        [Required(ErrorMessage = "Password confirmation is required.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsSuccess { get; set; }
        public string StatusMsg { get; set; }

    }
}