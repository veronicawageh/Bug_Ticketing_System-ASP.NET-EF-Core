using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;


namespace BugTicketingSystem.DAL 
{ 
    public class UnitOfWork : IunitOfWork
    {
        public IUserRepo UserRepo { get; }
        public IProjectRepo ProjectRepo { get; }
        public IBugRepo BugRepo { get; }
        public IuserBugRepo UserBugRepo { get; }
        public IAttachementRepo AttachementRepo { get; }
        private readonly BugTicketingContext context;
        public UnitOfWork(IUserRepo userRepo ,BugTicketingContext _context
            ,IProjectRepo projectRepo , IBugRepo bugRepo ,  IuserBugRepo userBug , IAttachementRepo attachementRepo)
        {
            UserRepo = userRepo;
            context = _context;
            ProjectRepo = projectRepo;
            BugRepo = bugRepo;
            UserBugRepo = userBug;
            AttachementRepo = attachementRepo;
        }
        public async Task<int> SaveChangesAsync()
        {
           return await context.SaveChangesAsync();
        }
    }
}
