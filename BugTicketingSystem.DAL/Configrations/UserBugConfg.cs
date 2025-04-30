using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configrations
{
    public class UserBugConfg : IEntityTypeConfiguration<UserBug>
    {
        public void Configure(EntityTypeBuilder<UserBug> builder)
        {
           builder.HasKey(ub=> new {ub.UserId,ub.BugId});
            #region one to many (bug,user)
            builder.HasOne(ub => ub.User)
                .WithMany(u => u.UserBugs)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region one to many (bug,user)
            builder.HasOne(ub => ub.Bug)
                .WithMany(b => b.UserBugs)
                .HasForeignKey(ub=>ub.BugId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
