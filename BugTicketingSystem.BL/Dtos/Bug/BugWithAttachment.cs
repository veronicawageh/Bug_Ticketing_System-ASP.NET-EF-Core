using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Dtos.Bug
{
    public class BugWithAttachment
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string priority { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; }
      
        public List<FileDto> Files { get; set; }
    }
}
