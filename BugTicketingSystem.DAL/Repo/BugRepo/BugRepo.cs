using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class BugRepo :GenericRepo<Bug>, IBugRepo
    {
        private readonly BugTicketingContext context;
        public BugRepo(BugTicketingContext context) : base(context) { 
        
            this.context = context;
        }

        public  async Task<Bug?> GetBugDetials(Guid bugId)
        {
            return await context.Set<Bug>()
    .Include(b => b.Project)
    .Include(b => b.Files)
    .Include(b => b.UserBugs)
        .ThenInclude(ub => ub.User).FirstOrDefaultAsync(b=>b.Id==bugId);
        }
        public async Task<Bug?> GetBugWithAttatchments(Guid id)
        {
            return await context.Set<Bug>().Include(b => b.Files).FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}
