using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using FluentValidation;

namespace BugTicketingSystem.BL
{
   public class AddBugValidator: AbstractValidator<AddBugDto>
    {
        private readonly IunitOfWork _unitOfWork;
       

        public AddBugValidator(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(b => b.Title
            ).NotEmpty()
            .WithMessage("Title cannot be empty .")
                .MaximumLength(50)
                .WithMessage("Max lenght for name is 50 letter .");
                //.MustAsync(CheckIfBugExist)
                //.WithMessage("This Bug is exist .");

            RuleFor(b => b.Description)
                .NotEmpty()
                .WithMessage("Title cannot be empty .")
                .MaximumLength(500)
                 .WithMessage("Max lenght for description is 50 letter .");


            RuleFor(b => b.Status)
               
              .Must(status => ValidatorHelper.IsValidEnumValues<BugStatus>(status))
             .WithMessage("State is invalid.")
             .NotEmpty()
             .WithMessage("State can not be empty");




            RuleFor(b => b.priority)

              .Must(pro=>ValidatorHelper.IsValidEnumValues<BugPriority>(pro))
              
             .WithMessage("priority is invalid.")
             .NotEmpty()
             .WithMessage("priority can not be empty");

            RuleFor(b => b.ProjectId)
                   .NotEmpty()
                   .WithMessage("Doctor id can not be empty")
                .MustAsync(ifProjectExist)
                .WithMessage("This project do not exist .");


        }
        //private async Task<bool> CheckIfBugExist(string arg, Guid id, CancellationToken token)
        //{
        //    var Bugs = await _unitOfWork.BugRepo.GetAllAsync();

        //    var Project = await _unitOfWork.ProjectRepo.GetAllAsync();


        //    return Bugs.All(b => b.Title != arg);
        //}
        private async Task<bool> ifProjectExist(Guid id, CancellationToken token)
        {
            var projects = await _unitOfWork.ProjectRepo.GetByIdAsync(id);
            if (projects == null)
            {

                return false;
            }
            return true;

        }
    }
    }
