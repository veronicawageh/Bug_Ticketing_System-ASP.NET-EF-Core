using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;

namespace BugTicketingSystem.DAL
{
    public interface IProjectRepo : IGenricRepo<Project>
    {
        Task<Project?> GetProjectWithDetails(Guid id);
    }
}
