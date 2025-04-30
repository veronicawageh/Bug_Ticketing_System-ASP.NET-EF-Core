using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BugTicketingSystem.BL
{
    public class ProjectAddValidator : AbstractValidator<AddProjectDto>
    {
        private readonly IunitOfWork _unitOfWork;
        public ProjectAddValidator(IConfiguration configuration, IunitOfWork unitOfWork)
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Name cannot be empty .")
                .MaximumLength(50)
                .WithMessage("Max lenght for name is 50 letter .")
                .MustAsync(CheckIfProjectExist)
                .WithMessage("This project is exist .");
            RuleFor(p => p.Description).NotEmpty()
                .WithMessage("Description cannot be empty .")
                .MaximumLength(500)
                .WithMessage("Max lenght for description is 500 letter .");
            _unitOfWork = unitOfWork;
        }
        private async Task<bool> CheckIfProjectExist(string arg, CancellationToken token)
        {
            var projects = await _unitOfWork.ProjectRepo.GetAllAsync();
            return projects.All(p=>p.Name != arg);
        }
    }
}
