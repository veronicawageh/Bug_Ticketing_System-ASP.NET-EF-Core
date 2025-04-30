using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public  string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Bug> Bugs { get; set; } = [];
    }
}
