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
    public class bugsController : ControllerBase
    {
        private readonly IUserBugManager _Manager;
        private readonly IBugManager _BugManager;
        private readonly IAttachementManager _attachementManager1;
        
        public bugsController(IUserBugManager manager,IAttachementManager attachementManager , IBugManager bugManager)
        {
            _Manager = manager;
            _attachementManager1 = attachementManager;
            _BugManager = bugManager;
        }
        [HttpPost("{bugId}/assignees")]
        public async Task<Results<Ok<GeneralResult>,BadRequest<GeneralResult>>>  AssignUserToBug( Guid bugId ,[FromBody] string userId)
        {
            UserBugDto ub= new UserBugDto { UserId = userId, BugId = bugId };
            var Result= await _Manager.AddUserToBug(ub);
            if (Result.Success) {
            
            return TypedResults.Ok(Result);
            }
            return TypedResults.BadRequest(Result);
         
        }
        [HttpDelete("{bugId}/assignees/{userId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> DeleteUserFRomBug(Guid bugId , string userId)
        {
            UserBugDto bu= new UserBugDto { UserId= userId, BugId = bugId };
            var result= await _Manager.RemoveUserFromBug(bu);
            if (result.Success) { 
            return TypedResults.Ok(result);
            
            }
            return TypedResults.BadRequest(result);
        }
        //----------------------------------------------------add attachement to Bug ------------------------------------------------------------------------------//
        [HttpPost("{bug_Id}/attachments")]

        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> AddAsync(Guid bug_Id, [FromForm] FileUploadRequest fileRequest)
        {
            var result = await _attachementManager1.AddAttachementAsync(bug_Id, fileRequest);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }

        [HttpDelete("{bugId}/attachments/{AttachmentId}")]
        public async Task<Results<Ok<GeneralResult>, BadRequest<GeneralResult>>> DeleteUserFRomBug(Guid bugId,Guid AttachmentId)
        {
            var result= await _attachementManager1.RemoveAttachmentfrombug(bugId, AttachmentId);
            if (result.Success) { 
            
            return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);

        }
        [HttpGet("{id}")]
        public async Task<Results<Ok<GeneralResult<BugDeitails?>>, NotFound<GeneralResult<BugDeitails?>>>> GetBugId(Guid id)
        {
            var result = await _BugManager.GetBugDeitals(id);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.NotFound(result);
        }


        [HttpGet("{bug_Id}/attachments")]

        public async Task<Results<Ok<GeneralResult<BugWithAttachment?>>, BadRequest<GeneralResult<BugWithAttachment?>>>> GetBugwithAttachmentAsync(Guid bug_Id)
        {
            var result = await _BugManager.GetWithAttachment(bug_Id);

            if (result.Success)
            {
                return TypedResults.Ok(result);
            }
            return TypedResults.BadRequest(result);
        }
    }
}
