using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;


namespace BugTicketingSystem.DAL
{
    public interface IunitOfWork
    {
        public IUserRepo UserRepo { get; }
        public IProjectRepo ProjectRepo { get; }
        public IBugRepo BugRepo { get; }
        public IuserBugRepo UserBugRepo { get; }
        public IAttachementRepo AttachementRepo { get; }
        Task<int > SaveChangesAsync();
    }
}
