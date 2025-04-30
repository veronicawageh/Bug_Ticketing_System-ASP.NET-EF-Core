using BugTicketingSystem.BL;
using BugTicketingSystem.BL.Dtos.Bug;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly IBugManager _bugManager;
        public BugController(IBugManager bugManager)
        {
            _bugManager = bugManager;
        }
        [HttpPost]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddBug(AddBugDto addBug)
        {
            var Result = await _bugManager.AddBug(addBug);
            if (!Result.Success)
            {
                return TypedResults.BadRequest(Result);
            }
            return TypedResults.Ok(Result);

        }
        [HttpGet]
        public async Task<Results<Ok<GeneralResult<ViewBugDto[]?>>, NotFound<GeneralResult<ViewBugDto[]?>>>> GetAllBugs()
        {
            var Result = await _bugManager.GetAll();

            if (Result.Success)
            {
                return TypedResults.Ok(Result);
                
            }

            return TypedResults.NotFound(Result);
        }

      
    }
}