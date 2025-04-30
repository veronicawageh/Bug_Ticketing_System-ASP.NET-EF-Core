using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
    [Flags]
   public enum BugStatus
    {
        None = 0,
        Opened = 1,
        InProcess = 2,
        Resolved = 4
    }
}
