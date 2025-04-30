using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL;

namespace BugTicketingSystem.BL
{
    public interface IMyUserManager
    {
        public Task<GeneralResult> ValidateDto(RegisterDto RegisterUser);
        public Task<GeneralResult<TokenDto?>> ValidateLoginUser(LoginDto LoginUser);   
        
        Task<ViewUserDto[]?> GetAll();
        Task<bool> Delete(string id);
        Task<ViewUserDto?> GetByIdAsync(string id);
        TokenDto GenerateToken(List<Claim> claims);
    }
}
