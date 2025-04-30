using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.DAL
{
    public static  class DALExtentaions
    {
        public static void AddDALServices
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BugTicketingContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddScoped<IunitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IBugRepo, BugRepo>();
            services.AddScoped<IProjectRepo, projectRepo>();
            services.AddScoped<IAttachementRepo, AttachementRepo>();
            services.AddScoped<IuserBugRepo,UserBugRepo>(); 

        }
    }
}
