using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
   public class UserBugRepo:GenericRepo<UserBug>,IuserBugRepo
    {
        private readonly BugTicketingContext _context;
        public UserBugRepo(BugTicketingContext context
            ):base(context) {
        _context = context;
        }

        public async Task<UserBug?> GetUserbug(string userid,Guid bugid)
        {
            return await _context.Set<UserBug>().FirstOrDefaultAsync(b=>b.UserId==userid && b.BugId==bugid);
            
        }
    }
}
