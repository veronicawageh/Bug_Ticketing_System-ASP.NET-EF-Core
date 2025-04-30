using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class UserRepo : GenericRepo<CustomUser>, IUserRepo
    {
        private readonly BugTicketingContext context;
        public UserRepo(BugTicketingContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<CustomUser?> Login(CustomUser user
            )
        {
            return await context.Set<CustomUser>().FirstOrDefaultAsync(u=>u.Email== user.Email);   
        }

        public  void Register(CustomUser user)
        {
             context.Set<CustomUser>().Add(user);
        }
        public async Task<CustomUser?> GetByIdAsync(string id)
        {
            return await context.Set<CustomUser>().FindAsync(id);
        }

    }
}
