using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class BugTicketingContext : IdentityDbContext<CustomUser>
    {
        public BugTicketingContext(DbContextOptions<BugTicketingContext> options) : base(options)
        { }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BugTicketingContext).Assembly);

        }

        #region Tables
        public DbSet<Project> Projects
            => Set<Project>();
        public DbSet<Attachement> Attachements=>Set<Attachement>();
        public DbSet<Bug> Bugs =>Set<Bug>();
        public DbSet<UserBug> userBugs =>Set<UserBug>();
        #endregion
    }
}
