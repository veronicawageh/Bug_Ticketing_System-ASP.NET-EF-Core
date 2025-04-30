using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class TokenDto
    {
        public required string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
