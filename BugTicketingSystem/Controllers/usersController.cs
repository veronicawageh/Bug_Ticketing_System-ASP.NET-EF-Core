using System.Security.Claims;
using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Validators.User;
using BugTicketingSystem.DAL;
using BugTicketingSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IMyUserManager myUserManager;
        public usersController(
          UserRegisterValidator userRegisterValidator,
          IMyUserManager myUserManager)
        {
            this.myUserManager = myUserManager;
        }
        #region Register 

        [HttpPost]
        [Route("register")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>>
              Register(RegisterDto userReigster)
        {
            var result = await myUserManager.ValidateDto(userReigster);
            if (result.Success)
            {
                return TypedResults.Ok(result);
            }

            return TypedResults.BadRequest(result);
        }
        #endregion
        #region Get All
        [HttpGet]
        public async Task<Results<Ok<GeneralResult<ViewUserDto[]>>, BadRequest<GeneralResult>>> GetUsers()
        {
            var users = await myUserManager.GetAll();
            if (users == null)
            {
                return TypedResults.BadRequest(new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[]
                    {
                new ResultError { Message = "No users found." }
                    }
                });
            }

            return TypedResults.Ok(new GeneralResult<ViewUserDto[]>
            {
                Success = true,
                Errors = null,
                Data = users
            });
        }
        #endregion
        #region GetById

        [HttpGet("{id}")]
        public async Task<Results<Ok<GeneralResult<ViewUserDto>>, BadRequest<GeneralResult>>> GetUserById(string id)
        {
            var user = await myUserManager.GetByIdAsync(id);
            if (user == null)
            {
                return TypedResults.BadRequest(new GeneralResult
                {
                    Success = false,
                    Errors = new ResultError[]
            {
                new ResultError { Message = "User not found ." }
            }
                });
            }
            return TypedResults.Ok(new GeneralResult<ViewUserDto>
            {
                Success = true,
                Errors = null,
                Data = user
            });
        }
        #endregion
        #region DeleteById

        [HttpDelete("{id}")]
        public async Task<Results<Ok<GeneralResult>, NotFound<GeneralResult>>> DeleteById(string id
            )
        {
            bool result = await myUserManager.Delete(id);
            if (!result)
            {

                return TypedResults.NotFound(
                    new GeneralResult
                    {
                        Success = false,
                        Errors = new ResultError[] { new ResultError { Message = "User not found or could not be deleted." } }
                    });
            }
            return TypedResults.Ok(new GeneralResult
            {
                Success = true,
                Errors = null
            });
        }
        #endregion
        #region Login
        [HttpPost]
        [Route("login")]
        public async Task<Results<Ok<GeneralResult<TokenDto?>>, BadRequest<GeneralResult<TokenDto?>>>> Login(LoginDto loginDto)
        {
            var Result = await myUserManager.ValidateLoginUser(loginDto);

            if (Result.Success)
            {
                return TypedResults.Ok(Result);
            }
            return TypedResults.BadRequest(Result);
        }
            #endregion
        }
    }
