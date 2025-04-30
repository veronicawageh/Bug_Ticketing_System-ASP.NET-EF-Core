using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.DAL
{
   public class Attachement
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string URL { get; set; }
        //public required string Path { get; set; }
        #region one to many (bug, file)
        public Guid? BugId { get; set; }
        public Bug Bug { get; set; } = null!;
        #endregion

    }
}
