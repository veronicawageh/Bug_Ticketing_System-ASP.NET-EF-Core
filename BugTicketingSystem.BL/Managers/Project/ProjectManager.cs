using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Dtos.Bug;
using BugTicketingSystem.DAL;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTicketingSystem.BL
{
    public class ProjectManager : IProjectManager
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly ProjectAddValidator _validations;


        public ProjectManager(IunitOfWork uintofwork, ProjectAddValidator validations)
        {
            _unitOfWork = uintofwork;
            _validations = validations;
        }

        public async Task<GeneralResult> AddProject(AddProjectDto Project)
        {
            var ValidateProject = await _validations.ValidateAsync(Project);
            if (!ValidateProject.IsValid)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = ValidateProject.Errors.Select(e => new ResultError { Message = e.ErrorMessage }).ToArray()
                };
            }
            Project p = new Project
            {
                Name = Project.Name,
                Description = Project.Description,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.ProjectRepo.Add(p);
          var saveResult=  await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new GeneralResult
                {
                    Success = true,
                    Errors = null
                };
            }
            else
            {

                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message =" unable to add this project"}
    }

                };




            };
        }
        public async Task<GeneralResult<ViewProjectDto[]?>> GetAll()
        {
            var ProjectsFromDb = await _unitOfWork.ProjectRepo.GetAllAsync();
            if (ProjectsFromDb == null || !ProjectsFromDb.Any())
            {
                return new GeneralResult<ViewProjectDto[]?>
                {

                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "No Data Found" } },
                    Data = null
                };
            }

            return new GeneralResult<ViewProjectDto[]?>
            {
                Success = true,
                Errors = null,
                Data =

                ProjectsFromDb.Select(u => new ViewProjectDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    CreatedAt = u.CreatedAt,
                }).ToArray()

            };
        }

        public async Task<ViewProjectDto?> GetById(Guid id)
        {
            var project = await _unitOfWork.ProjectRepo.GetByIdAsync(id);
            if (project == null)
            {
                return null;
            }
            return new ViewProjectDto
            {

                Id = id,
                Name = project.Name,
                Description = project.Description ?? string.Empty,
                CreatedAt = project.CreatedAt

            };
        }

        public async Task<GeneralResult<ProjectDetialsDto?>> GetProjectDetials(Guid Id)
        {
            var ProjectFromDb = await _unitOfWork.ProjectRepo.GetProjectWithDetails(Id);
            if (ProjectFromDb == null)
            {
                return new GeneralResult<ProjectDetialsDto?>
                {
                    Success = false,
                    Errors = new ResultError[] {new ResultError
                    {
                        Message="Project not found ."
                    } },
                };
            }

            return new GeneralResult<ProjectDetialsDto?>
            {
                Success = true,
                Errors = null,
                Data =
                new ProjectDetialsDto
                {
                    Id = ProjectFromDb.Id,
                    Name = ProjectFromDb.Name,
                    Description = ProjectFromDb.Description,
                    CreatedAt = ProjectFromDb.CreatedAt,
                    Bugs = ProjectFromDb.Bugs.Select(b => new BugInProject
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        CreatedAt = b.CreatedAt,
                        Status = b.Status.ToString(),
                        priority = b.priority.ToString(),
                    }).ToArray(),

                }
            };
        }
    }
}
