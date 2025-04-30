using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Dtos.Bug;

namespace BugTicketingSystem.BL
{
    public interface IBugManager
    {
        Task<GeneralResult> AddBug(AddBugDto Bug);
        Task<GeneralResult<ViewBugDto[]?>> GetAll();
        Task<GeneralResult<BugDeitails?>> GetBugDeitals(Guid id);
        Task<GeneralResult<BugWithAttachment?>> GetWithAttachment
            (Guid id);
  
    }
}
