using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem.DAL
{
    public class Bug
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public BugPriority priority { get; set; }
        public BugStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        #region one to many relation with project
        public Guid ProjectId{ get; set; }
        public Project Project { get; set; } = null!;
        #endregion
        #region many to many (user,bug)
        public ICollection<UserBug> UserBugs { get; set; } = [];
        #endregion
        #region
        public ICollection<Attachement> Files { get; set; } = [];
        #endregion

    }
}
