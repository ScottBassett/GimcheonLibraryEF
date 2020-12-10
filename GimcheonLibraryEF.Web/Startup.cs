using System;
using GimcheonLibraryEF.DataAccess;
using GimcheonLibraryEF.DataAccess.Models;
using GimcheonLibraryEF.Web.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GimcheonLibraryEF.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "145350645045-n0miv3hq065qehlodd8reh7evgk9u8ri.apps.googleusercontent.com";
                    options.ClientSecret = "2A2XClaY8Ix2OygXM-Lezk90";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "428047571552996";
                    options.AppSecret = "5b5632b056329491e77934989556b8f7";
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Administration/AccessDenied");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });

            services.AddDbContext<GimcheonLibraryDbContext>(options =>
                options.UseNpgsql(
                    _config.GetConnectionString("PostgresConnection")));

           services.AddIdentity<ApplicationUser, IdentityRole>(options =>
               {
                   options.Password.RequiredLength = 5;
                   options.Password.RequiredUniqueChars = 3;

                   options.SignIn.RequireConfirmedEmail = true;

                   options.Lockout.MaxFailedAccessAttempts = 5;
                   options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
               })
               .AddEntityFrameworkStores<GimcheonLibraryDbContext>()
               .AddDefaultTokenProviders();

           services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
           services.AddSingleton<IAuthorizationHandler, SuperAdminHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GimcheonLibraryDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            db.Database.Migrate();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
