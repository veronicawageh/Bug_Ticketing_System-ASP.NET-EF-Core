using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
    [Flags]
    public enum BugPriority
    {
       NONE=0, LOW=1, MEDIUM=2, HIGH=3
    }
}
