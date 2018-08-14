using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LS.Core.Model;
using System.ComponentModel.DataAnnotations;
using PJX.Core.Model;

namespace AM.Web.Areas.Account.Models {
    public class ProfileViewModel {

        public User Profile { get; set; }

        [Required(ErrorMessage = "Your first name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Your last name is required.")]
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required(ErrorMessage = "Your address is required.")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessage = "Your city is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Your state is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Your zipcode is required.")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Your country is required.")]
        public string Country { get; set; }

        public bool IsEmailOptIn { get; set; }
        public string TimeZoneId { get; set; }

        public bool IsSuccess { get; set; }
        public string StatusMsg { get; set; }

        public ProfileViewModel() {
        }

        public ProfileViewModel(User u) {
            FirstName = u.FirstName;
            LastName = u.LastName;
            Company = u.CompanyName;
            Address1 = u.Address.Address1;
            Address2 = u.Address.Address2;
            City = u.Address.City;
            State = u.Address.State;
            ZipCode = u.Address.PostalCode;
            Country = u.Address.CountryCode;

            IsEmailOptIn = u.IsEmailOptIn;
            TimeZoneId = u.TimeZoneId;

            Profile = u;
        }

    }
}