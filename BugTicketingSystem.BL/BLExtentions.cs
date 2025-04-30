using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL
{
    public static class BLExtentions
    {
        public static void  AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMyUserManager,MyUserManager>();
            services.AddScoped<IProjectManager,ProjectManager>();
            services.AddScoped<IBugManager,BugManager>();
            services.AddScoped<IUserBugManager,UserBugManager>();
            services.AddScoped<IAttachementManager, AttachementManager>();
            services.AddValidatorsFromAssembly(

               typeof(BLExtentions).Assembly);
        }
    }
    
}
