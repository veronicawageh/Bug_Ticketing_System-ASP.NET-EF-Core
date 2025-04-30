using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Validators.User;
using BugTicketingSystem.DAL;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.BL
{
    public class MyUserManager : IMyUserManager
    {
        private readonly UserRegisterValidator _userRegisterValidator;
        private readonly UserLoginValidator _userLoginValidator;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IunitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        public MyUserManager(UserRegisterValidator userRegisterValidator
            , UserManager<CustomUser> userManager,
              IunitOfWork unitOfWork,UserLoginValidator userLoginValidator
            , IConfiguration configuration)
        {
            _userRegisterValidator = userRegisterValidator;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _userLoginValidator = userLoginValidator;
        }



        public async Task<bool> Delete(string id)
        {
            var userfromdb = await _userManager.FindByIdAsync(id);
            if (userfromdb == null)
            {
                return false;
            }
            _unitOfWork.UserRepo.Delete(userfromdb);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public TokenDto GenerateToken(List<Claim> claims)
        {
            var secretKey = _configuration.GetValue<string>("ScretKey")!;
            var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            )
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenDto { Token = tokenString, ExpireDate = token.ValidTo };
        }

        public async Task<ViewUserDto[]?> GetAll()
        {

            var usersFromDb = await _unitOfWork.UserRepo.GetAllAsync();
            if (usersFromDb == null || !usersFromDb.Any())
            {
                return null;
            }

            return usersFromDb.Select(u => new ViewUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role.ToString("G")
            }).ToArray();


        }

        public async Task<ViewUserDto?> GetByIdAsync(string id)
        {
            var userfromdb = await _unitOfWork.UserRepo.GetByIdAsync(id);
            if (userfromdb == null) { return null; }
            return new ViewUserDto
            {
                Id = userfromdb.Id,
                UserName = userfromdb.UserName,
                Email = userfromdb.Email,
                Role = userfromdb.Role.ToString("G")
            };
        }

        public async Task<GeneralResult<TokenDto?>> ValidateLoginUser(LoginDto loginUser)
        {
            var validateUser = await _userLoginValidator.ValidateAsync(loginUser);
            if (!validateUser.IsValid)
            {
                return new GeneralResult<TokenDto?>
                {
                    Success = false,
                    Errors = validateUser.Errors
                   .Select(e => new ResultError
                   {
                       Message = e.ErrorMessage

                   }).ToArray()
                   ,Data= null
                };

            }
            var userFromDb = await _userManager.FindByEmailAsync(loginUser.Email);

            if (userFromDb == null)
            {

                return new GeneralResult<TokenDto?>
                {
                    Success = false,
                    Errors = new ResultError[]
                     {
                new ResultError { Message = "Invalid Email or Password." }
                     },Data= null
                };
            }

            var correctPassword = await _userManager.CheckPasswordAsync(userFromDb, loginUser.Password);
            if (!correctPassword)
            {
                return new GeneralResult<TokenDto?>
                {
                    Success = false,
                    Errors = new ResultError[]
                     {
                new ResultError { Message = "Invalid Email or Password." }
                     }
                     ,Data= null
                };
            }
            var claims = await _userManager.GetClaimsAsync(userFromDb);
            var tokenDto = GenerateToken(claims.ToList());


            return new GeneralResult<TokenDto?>
            {
                Success = true,
                Errors = null,
                Data = tokenDto
            };
        }

        public async Task<GeneralResult> ValidateDto(RegisterDto RegisterUser)
        {
            var validateUser = await _userRegisterValidator.ValidateAsync(RegisterUser);
            if (!validateUser.IsValid)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = validateUser.Errors
                   .Select(e => new ResultError
                   {
                       Message = e.ErrorMessage

                   }).ToArray()
                };

            }
            UserRoles parsedRoles = RegisterUser.Role
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(r => Enum.Parse<UserRoles>(r.Trim(), true))
                .Aggregate((a, b) => a | b); // Combines them with bitwise OR
            var userToadd = new CustomUser
            {
                UserName = RegisterUser.UserName,
                Email = RegisterUser.Email,
                Role = parsedRoles,
            };

            #region Hashed password check
            var creationResult = await _userManager.CreateAsync(userToadd, RegisterUser.Password);
            if (!creationResult.Succeeded)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = creationResult.Errors.ToList()
                  .Select(e => new ResultError
                  {
                      Message = e.Description

                  }).ToArray()
                };
            }
            #endregion
            //    var claims = new List<Claim>
            //{
            //    new (ClaimTypes.NameIdentifier, userToadd.Id),
            //    new (ClaimTypes.Email, userToadd.Email),
            //    new (ClaimTypes.Role,  RegisterUser.Role)
            //};
            var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, userToadd.Id),
    new Claim(ClaimTypes.Email, userToadd.Email)
};

            var selectedRoles = RegisterUser.Role
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim());

            foreach (var role in selectedRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            await _userManager.AddClaimsAsync(userToadd, claims);
            return new GeneralResult
            {
                Success = true,
                Errors = null
            };
        }


    }
}