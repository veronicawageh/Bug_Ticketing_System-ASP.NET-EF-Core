using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    public class FileConfg : IEntityTypeConfiguration<Attachement>
    {
        public void Configure(EntityTypeBuilder<Attachement> builder)
        {
           builder.HasKey(e => e.Id);
            builder.Property(f=>f.Title).IsRequired()
                .IsUnicode()
                .HasMaxLength(100);
            builder.Property(f => f.URL).IsRequired()
                .IsUnicode()
                .HasMaxLength(500);
            #region one to many(Bug,File)
            builder.HasOne(f => f.Bug)
                .WithMany(b => b.Files)
                .HasForeignKey(f => f.BugId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}
