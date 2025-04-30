using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configrations
{
    public class BugConfg : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Title).IsRequired()
                .IsUnicode()
                .HasMaxLength(50);
            builder.Property(b => b.Description).IsRequired()
               .IsUnicode()
               .HasMaxLength(500);

            builder.Property(b => b.Status)
                 .HasConversion(
                     v => v.ToString(),                // to DB (as string)
                     v => (BugStatus)Enum.Parse(typeof(BugStatus), v)); // from DB

            builder.Property(b => b.priority)
                 .HasConversion(
                     v => v.ToString(),                // to DB (as string)
                     v => (BugPriority)Enum.Parse(typeof(BugPriority), v)); // from DB
            #region
            builder.HasOne(b => b.Project)
                .WithMany(p => p.Bugs)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion one to many (bug,file)
            builder.HasMany(b=>b.Files)
                .WithOne(f=>f.Bug)
                .HasForeignKey(f=>f.BugId)
                .OnDelete(DeleteBehavior.Cascade);
            #region

            #endregion
            builder.Property(e => e.CreatedAt)
          .HasDefaultValueSql("GETDATE()");
        }
    }
}
