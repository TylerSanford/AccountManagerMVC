using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Model.Enums {
    public enum AccountStatuses {
        Approved,
        Locked,
        Denied,
        Unlocked
    }

    public enum ViewModes {
        Register,
        Create,
        Edit,
        Delete,
        Search
    }

    public enum SearchOptions {
        Email,
        Role,
        None
    }

    public enum ViewingPage {
        Home,
        Login,
        
        UserList,
        UserRegister,
        UserCreate,
        UserEdit,
        UserChangePassword,
        UserDelete,

        RoleList,
        RoleCreate,
        RoleEdit,
        RoleDelete,
    }
}
