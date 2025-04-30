using System.Data;
using BugTicketingSystem.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase

    {
        private readonly IProjectManager _projectManager;
        public ProjectController(IProjectManager projectManager)
        {
            _projectManager = projectManager;
        }
        #region Get all projects
        [HttpGet]
        [Authorize]
        public async Task<Results<Ok<GeneralResult<ViewProjectDto[]?>>, NotFound<GeneralResult<ViewProjectDto[]?>>>> GetAllProjects()
        {
            var Projects = await _projectManager.GetAll();
            if (Projects.Success)
            {
                return TypedResults.Ok(Projects);
            }
            return TypedResults.NotFound(Projects);
        }
        #endregion
        #region Get project

        [HttpGet("{id}")]
        public async Task<Results<Ok<GeneralResult<ProjectDetialsDto?>>, NotFound<GeneralResult<ProjectDetialsDto?>>>> GetById(Guid id)
        {
            var project = await _projectManager.GetProjectDetials(id);
            if (project.Success)
            {
                return TypedResults.Ok(project);
            }
            return TypedResults.NotFound(project);
        }
        #endregion
        #region Add project

        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddProject(AddProjectDto projectDto)
        {
            var result = await _projectManager.AddProject(projectDto);
            if (result.Success)
            {

                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
        #endregion
    }
}