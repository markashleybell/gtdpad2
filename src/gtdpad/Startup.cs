using System;
using System.Linq;
using System.Security.Claims;
using gtdpad.Controllers;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static gtdpad.Functions;

namespace gtdpad
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            const string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            var connectionString = Configuration.GetConnectionString("Main");

            services.AddHttpContextAccessor();

            services.Configure<Settings>(Configuration);

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = _ => false;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddAuthentication(authenticationScheme)
                .AddCookie(authenticationScheme, options => {
                    options.LoginPath = GetUrl(nameof(UsersController), nameof(UsersController.Login));
                    options.LogoutPath = GetUrl(nameof(UsersController), nameof(UsersController.Logout));
                });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<IRepository, SqlServerRepository>(sp => {
                var ctxAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var optionsMonitor = sp.GetRequiredService<IOptionsMonitor<Settings>>();

                var sidClaim = ctxAccessor.HttpContext.User.Claims
                    .SingleOrDefault(c => c.Type == ClaimTypes.Sid);

                var userId = sidClaim is object
                    ? new Guid(sidClaim.Value)
                    : default(Guid?);

                return new SqlServerRepository(optionsMonitor);
            });

            services.AddControllersWithViews();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
