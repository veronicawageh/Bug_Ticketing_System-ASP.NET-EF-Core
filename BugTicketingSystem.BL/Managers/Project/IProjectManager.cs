using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;

namespace BugTicketingSystem.BL
{ 
    public interface IProjectManager
    {
        Task<GeneralResult<ViewProjectDto[]?>> GetAll();
        Task<GeneralResult> AddProject(AddProjectDto Project);
        Task<ViewProjectDto?> GetById(Guid id);  
        Task<GeneralResult<ProjectDetialsDto?>> GetProjectDetials(Guid Id);
        
    }
};
