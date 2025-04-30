
using System.Security.Claims;
using System.Text;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Scalar.AspNetCore;

namespace BugTicketingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDALServices(builder.Configuration);
            builder.Services.AddServices();
           
            #region identity registretion

            builder.Services.AddIdentityCore<CustomUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<BugTicketingContext>();
            #endregion
            #region Authentication

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var secretKey = builder.Configuration.GetValue<string>("ScretKey")!;

                    var secretKeyInBytes = Encoding.UTF8.GetBytes(secretKey);
                    var key = new SymmetricSecurityKey(secretKeyInBytes);

                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key,
                    };
                });

            #endregion
            #region authrezation
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ForAdmin, policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Admin", "Manager"));

                options.AddPolicy(Policies.ForAgent, policy =>
                    policy.RequireClaim(ClaimTypes.Role, "Developer"));
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }
            var imagesFolder = Path.Combine(
    Directory.GetCurrentDirectory(),
    "Images");

            // if folder is not created, create it
            Directory.CreateDirectory(imagesFolder);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(imagesFolder),
                RequestPath = "/api/my-static-files"
            });
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
