using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class UserBugManager : IUserBugManager
    {
        private readonly IunitOfWork _uitOfWork;
        public UserBugManager(IunitOfWork uitOfWork)
        {
            _uitOfWork = uitOfWork;
        }
        public async Task<GeneralResult> AddUserToBug(UserBugDto userBug)
        {
            var user = await _uitOfWork.UserRepo.GetByIdAsync(userBug.UserId);
            if (user == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This user dose not exist" }
                    }

                };
            }
            var Bug = await _uitOfWork.BugRepo.GetByIdAsync(userBug.BugId);
            if (Bug == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This Bug dose not exist" }
                    }
                };

            }
            UserBug b = await _uitOfWork.UserBugRepo.GetUserbug(userBug.UserId, userBug.BugId);
            if (b != null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This user assigend to the bug before" }
                    }
                };
            }
            UserBug bu = new UserBug { UserId = userBug.UserId, BugId = userBug.BugId };
            _uitOfWork.UserBugRepo.Add(bu);
           var saveResult= await _uitOfWork.SaveChangesAsync();
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
                    Errors = new ResultError[] { new ResultError { Message =" unable to assign user to this bug"}
    }

                };




            };

        }

        public async  Task<GeneralResult> RemoveUserFromBug(UserBugDto userBug)
        {
            var user = await _uitOfWork.UserRepo.GetByIdAsync(userBug.UserId);
            if (user == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This user dose not exist" }
                    }

                };
            }
            var Bug = await _uitOfWork.BugRepo.GetByIdAsync(userBug.BugId);
            if (Bug == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This Bug dose not exist" }
                    }
                };

            }
           UserBug b= await _uitOfWork.UserBugRepo.GetUserbug(userBug.UserId ,userBug.BugId);
            if(b == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[] { new ResultError { Message = "This user donot assgin this bug to be deleted" }
                    }
                };
            }
            _uitOfWork.UserBugRepo.Delete(b);
           var saveResult = await _uitOfWork.SaveChangesAsync();

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
                    Errors = new ResultError[] { new ResultError { Message =" unable to remove user from this bug"}
    }

                };




            };
        }

      
    }
}