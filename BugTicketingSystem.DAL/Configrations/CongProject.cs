using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configrations
{
    public class CongProject : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsUnicode(true)
                .HasMaxLength(50)
                .IsRequired()
                ;
            builder.Property(e => e.Description).IsUnicode(true)
                .HasMaxLength(400)
                .IsRequired()
                ;
            builder.Property(e => e.CreatedAt)
             .HasDefaultValueSql("GETDATE()");
            builder.HasMany(p => p.Bugs)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
