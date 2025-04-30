using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL
{
    public class ConfUser : IEntityTypeConfiguration<CustomUser>
    {
        public void Configure(EntityTypeBuilder<CustomUser> builder)
        {
           builder.Property(u => u.Role)
                .HasConversion(
                    v => v.ToString(),                // to DB (as string)
                    v => (UserRoles)Enum.Parse(typeof(UserRoles), v)); // from DB
            builder.ToTable("Users");
            
        }
    }
}
