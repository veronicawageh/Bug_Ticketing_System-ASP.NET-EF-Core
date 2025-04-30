using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Identity;

namespace BugTicketingSystem.DAL
{
    public class CustomUser : IdentityUser
    {
        public UserRoles Role { get; set; }
        #region many to many (user,bug)
        public ICollection<UserBug> UserBugs { get; set; } = [];
        #endregion
    }
}
