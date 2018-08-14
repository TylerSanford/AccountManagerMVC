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
        Edit
    }
}
