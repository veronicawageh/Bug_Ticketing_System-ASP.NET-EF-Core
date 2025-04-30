using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
public class UserBug
    {
        public required string UserId { get; set; }
        public virtual CustomUser User { get; set; } = null!;
        public Guid BugId { get; set; }
        public virtual Bug Bug { get; set; } = null!;

    }
}
