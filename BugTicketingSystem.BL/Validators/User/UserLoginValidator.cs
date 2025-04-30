using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace BugTicketingSystem.BL.Validators.User
{
    public class UserLoginValidator : AbstractValidator<LoginDto>
    {
        private readonly IunitOfWork _unitOfWork;
        public UserLoginValidator(IunitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            RuleFor(u => u.Password)
               .NotEmpty()
               .WithMessage("Password cannot be empty");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .EmailAddress().WithMessage("Invalid email format");

        }
    }
}
