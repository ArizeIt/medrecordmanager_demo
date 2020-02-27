using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedRecordManager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrgentCareData;
using MedRecordManager.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using AdvancedMDService;
using AdvancedMDInterface;
using System.Threading.Tasks;
using MedRecordManager.Models.UserRecord;

namespace MedRecordManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

          
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(
                 Configuration.GetConnectionString("accountConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

           
            services.AddDbContext<UrgentCareContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;

                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                 .AddRazorPagesOptions(options =>
                 {
                     options.AllowAreas = true;
                     options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                     options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                 });

            services.AddDistributedMemoryCache();
            services.AddSession(opts =>
            {
                opts.IdleTimeout = TimeSpan.FromMinutes(20);
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<ILoginService, LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
          
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "MyArea",
                //    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        //private void CreateRoles(IServiceProvider serviceProvider)
        //{

        //    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        //    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //    Task<IdentityResult> roleResult;
        //    string email = "Danny.x.li@gmail.com";

        //    //Check that there is an Administrator role and create if not
        //    Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Administrator");
        //    hasAdminRole.Wait();

        //    if (!hasAdminRole.Result)
        //    {
        //        roleResult = roleManager.CreateAsync(new IdentityRole("Administrator"));
        //        roleResult.Wait();
        //    }

        //    //Check if the admin user exists and create it if not
        //    //Add to the Administrator role

        //    Task<ApplicationUser> testUser = userManager.FindByEmailAsync(email);
        //    testUser.Wait();

        //    if (testUser.Result == null)
        //    {
        //        ApplicationUser administrator = new ApplicationUser();
        //        administrator.Email = email;
        //        administrator.UserName = email;

        //        Task<IdentityResult> newUser = userManager.CreateAsync(administrator, "Danny.x.li@gmail.com");
        //        newUser.Wait();

        //        if (newUser.Result.Succeeded)
        //        {
        //            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(administrator, "Administrator");
        //            newUserRole.Wait();
        //        }
        //    }

        //}
    }
}
