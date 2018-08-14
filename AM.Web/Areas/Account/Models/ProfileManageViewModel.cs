using PJX.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AM.Web.Areas.Account.Models {
    public class ProfileManageViewModel {

        public List<User> Users { get; set; }

        public int PageId { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }

        public int UserCount { get; set; }

        // search settings
        public string SearchTerms { get; set; }
        public bool SearchName { get; set; }
        public bool SearchCompany { get; set; }
        public bool SearchPhone { get; set; }
        public bool SearchEmail { get; set; }
        public bool SearchRole { get; set; }

        // sort settings
        public bool SortByName { get; set; }
        public bool SortNameAsc { get; set; }       // true for asc, false for desc
        public bool SortByCompany { get; set; }
        public bool SortCompanyAsc { get; set; }    // true for asc, false for desc
        public bool SortByPhone { get; set; }
        public bool SortPhoneAsc { get; set; }      // true for asc, false for desc
        public bool SortByEmail { get; set; }
        public bool SortEmailAsc { get; set; }      // true for asc, false for desc
        public bool SortByApproval { get; set; }
        public bool SortApprovalAsc { get; set; }   // true for asc, false for desc

    }
}