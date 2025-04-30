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
    public class UserRegisterValidator : AbstractValidator<RegisterDto>
    {
        private readonly IunitOfWork _unitOfWork;
        public UserRegisterValidator(IunitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            #region Username

            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("User name cannot be empty")
                .MaximumLength(50)
                .WithMessage("Max lenght for name is 50 letter .");
            #endregion
            #region Password
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches(@"\d").WithMessage("Password must contain at least one number")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");
            #endregion
            #region Email
            RuleFor(u => u.Email).
              NotEmpty().WithMessage("Email is required")
              .EmailAddress().WithMessage("Invalid email format")
              .MustAsync(CheckEmailIsUnique)
              .WithMessage("Email is exist");
            #endregion
            #region Role
            RuleFor(u => u.Role)
            .Must(role =>
               {
                   var roles = role.Split(',', StringSplitOptions.RemoveEmptyEntries);
                   return roles.All(r => Enum.TryParse(typeof(UserRoles), r.Trim(), true, out _));
               })
             .WithMessage("One or more roles are invalid.")
             .NotEmpty()
             .WithMessage("Role required .");

            #endregion
        }
        private async Task<bool> CheckEmailIsUnique(string arg, CancellationToken token)
        {
            var users = await _unitOfWork.UserRepo.GetAllAsync();
            return users.All(d => d.Email != arg);
        }

    }
}
