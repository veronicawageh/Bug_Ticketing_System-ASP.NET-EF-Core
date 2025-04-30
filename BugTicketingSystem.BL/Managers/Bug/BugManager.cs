using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Dtos.Bug;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTicketingSystem.BL
{
    public class BugManager : IBugManager
    {
        private readonly IunitOfWork _unitOfWork;
        private readonly AddBugValidator _addValidator;
        public BugManager(IunitOfWork unitOfWork, AddBugValidator addValidator)
        {
            _unitOfWork = unitOfWork;
            _addValidator = addValidator;
        }

        public async Task<GeneralResult> AddBug(AddBugDto Bug)
        {
            var isValid = await _addValidator.ValidateAsync(Bug);
            if (!isValid.IsValid)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = isValid.Errors.Select(e => new ResultError { Message = e.ErrorMessage }).ToArray()
                };


            }
            var BugsFromDb = await _unitOfWork.BugRepo.GetAllAsync();
            var isExist = BugsFromDb.FirstOrDefault(b => b.Title == Bug.Title && b.ProjectId == Bug.ProjectId);
            if (isExist != null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This Bug has been assigend to the project" }
                    }

                };

            }
            BugStatus bugStatus = Bug.Status
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
               .Select(r => Enum.Parse<BugStatus>(r.Trim(), true))
                .Aggregate((a, b) => a | b);

            BugPriority bugPriority = Bug.priority
                 .Split(',', StringSplitOptions.RemoveEmptyEntries)
               .Select(r => Enum.Parse<BugPriority>(r.Trim(), true))
                .Aggregate((a, b) => a | b);
            Bug b = new Bug
            {
                Title = Bug.Title,
                ProjectId = Bug.ProjectId,
                Description = Bug.Description,
                Status = bugStatus,
                priority = bugPriority,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.BugRepo.Add(b);
            var saveResult = await _unitOfWork.SaveChangesAsync();
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
                    Errors = new ResultError[] { new ResultError { Message =" unable to add this bug"}
                    }
                };




            };


        }

        public async Task<GeneralResult<ViewBugDto[]?>> GetAll()
        {
            var BugsFromDb = await _unitOfWork.BugRepo.GetAllAsync();
            if (BugsFromDb == null || !BugsFromDb.Any())
            {
                return new GeneralResult<ViewBugDto[]?>
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "No data found ." } },
                    Data = null
                };
            }
            return new GeneralResult<ViewBugDto[]?>
            {
                Success = true,
                Errors = null,
                Data = BugsFromDb.Select(b => new ViewBugDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Status = b.Status.ToString(),
                    priority = b.priority.ToString(),
                    CreatedAt = DateTime.Now,
                    ProjectId = b.ProjectId,


                }).ToArray()
            };

        }
        public async Task<GeneralResult<BugDeitails?>> GetBugDeitals(Guid id)
        {
            var BugFromDb = await _unitOfWork.BugRepo.GetBugDetials(id);
            if (BugFromDb == null)
            {
                return new GeneralResult<BugDeitails?>
                {

                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "Bug not found ." }
                },
                    Data = null
                };
            }

            return new GeneralResult<BugDeitails?>
            {
                Success = true,
                Data = new BugDeitails
                {
                    Id = BugFromDb.Id,
                    Title = BugFromDb.Title,
                    Description = BugFromDb.Description,
                    Status = BugFromDb.Status.ToString(),
                    CreatedAt = BugFromDb.CreatedAt,
                    priority = BugFromDb.priority.ToString(),
                    ProjectName = BugFromDb.Project.Name,
                    Files = BugFromDb.Files.Select(s => new Dtos.FileDto { Id = s.Id, Title = s.Title, URL = s.URL }).ToList(),
                    Users = BugFromDb.UserBugs.Select(u => new ViewUserDto { Id = u.User.Id, Email = u.User.Email, Role = u.User.Role.ToString(), UserName = u.User.UserName }).ToList(),
                }
            };
        }

        public async Task<GeneralResult<BugWithAttachment?>> GetWithAttachment(Guid id)
        {
            var BugFromDb = await _unitOfWork.BugRepo.GetBugWithAttatchments(id);
            if (BugFromDb == null)
            {



                return new GeneralResult<BugWithAttachment?>
                {

                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "Bug not found ." },

                },
                    Data = null,
                };
            }
            return new GeneralResult<BugWithAttachment?>
            {
                Success = true,
                Errors = null,
                Data = new BugWithAttachment
                {
                    Id = BugFromDb.Id,
                    Title = BugFromDb.Title,
                    Description = BugFromDb.Description,
                    Status = BugFromDb.Status.ToString(),
                    CreatedAt = BugFromDb.CreatedAt,
                    priority = BugFromDb.priority.ToString(),

                    Files = BugFromDb.Files.Select(s => new Dtos.FileDto { Id = s.Id, Title = s.Title, URL = s.URL }).ToList(),

                }
            };
        }

    
    }
}
