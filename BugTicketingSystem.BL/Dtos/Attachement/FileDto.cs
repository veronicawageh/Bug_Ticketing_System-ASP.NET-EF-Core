using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL.Dtos
{
   public class FileDto
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string URL { get; set; }
    }
}
