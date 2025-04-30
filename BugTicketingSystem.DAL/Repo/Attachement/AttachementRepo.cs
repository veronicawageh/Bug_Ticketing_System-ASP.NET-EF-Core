using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
   public class AttachementRepo:GenericRepo<Attachement>,IAttachementRepo
    {
        private readonly BugTicketingContext context;
        public AttachementRepo(BugTicketingContext context) : base(context)
        {
            this.context = context;
        }

     
    }
}
