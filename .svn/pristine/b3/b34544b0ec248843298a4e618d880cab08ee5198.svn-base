using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Web.Areas.Account.Models {
    public class EmailViewModel {

        [Required(ErrorMessage = "Your email is required.")]
        public string Email { get; set; }

        public Guid RequestGuid { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public bool IsSuccess { get; set; }
        public string StatusMsg { get; set; }

    }
}
