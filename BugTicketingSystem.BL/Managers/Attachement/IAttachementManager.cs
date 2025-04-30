using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
     public interface IAttachementManager
    {
        Task<GeneralResult> AddAttachementAsync(Guid bug_id, FileUploadRequest fileRequest);
        Task<GeneralResult> RemoveAttachmentfrombug(Guid bugId, Guid AttatchmentId);
    }
}
