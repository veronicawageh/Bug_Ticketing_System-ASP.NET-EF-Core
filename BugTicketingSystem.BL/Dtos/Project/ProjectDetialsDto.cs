using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.BL.Dtos.Bug;
using BugTicketingSystem.DAL;

namespace BugTicketingSystem.BL
{
    public class ProjectDetialsDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<BugInProject> Bugs { get; set; } = [];
    }
}
