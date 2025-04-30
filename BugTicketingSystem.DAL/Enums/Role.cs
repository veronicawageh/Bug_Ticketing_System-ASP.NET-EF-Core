using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
    [Flags]
    public enum UserRoles
    {

        None = 0,
        Manager = 1,
        Developer = 2,
        Tester = 4
    }
}
