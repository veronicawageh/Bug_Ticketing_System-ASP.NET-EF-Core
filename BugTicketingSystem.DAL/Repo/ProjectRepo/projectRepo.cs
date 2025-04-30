using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class projectRepo : GenericRepo<Project>, IProjectRepo
    {
        private readonly BugTicketingContext _context;
        public projectRepo(BugTicketingContext context) : base(context)
        {
            {
                _context = context;
            }
        }

        public async Task<Project?> GetProjectWithDetails(Guid id)
        {
           var projectFromDb= await _context.Set<Project>().FindAsync(id);
            if(projectFromDb == null) {return null;}
            return await _context.Set<Project>()
        .AsNoTracking()
        .Include(p=>p.Bugs)
        .FirstOrDefaultAsync(b=>b.Id== id);
        }
        
    }
}
