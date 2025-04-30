using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
   public interface IBugRepo :IGenricRepo<Bug>
    {
        Task<Bug?> GetBugDetials(Guid bugId);
        Task<Bug?> GetBugWithAttatchments(Guid id);
      
    }
}
