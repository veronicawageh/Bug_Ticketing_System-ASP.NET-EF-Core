using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.DAL
{
    public interface IUserRepo : IGenricRepo<CustomUser>
    {
       
        Task<CustomUser?> GetByIdAsync(string id);
        public Task<CustomUser?> Login(CustomUser user);


    }
}
