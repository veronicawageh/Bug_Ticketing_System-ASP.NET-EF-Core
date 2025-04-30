using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class AddBugDto
    {

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string priority { get; set; }
        public required string Status { get; set; }
        public Guid ProjectId { get; set; }
    }
}
