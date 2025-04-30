using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;

namespace BugTicketingSystem.BL
{
    public interface IUserBugManager
    {
        Task<GeneralResult> AddUserToBug(UserBugDto userBug);
        Task<GeneralResult> RemoveUserFromBug(UserBugDto userBug); 
    }
}
