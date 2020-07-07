using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using gtdpad.Services;
using gtdpad.Support;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace gtdpad
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            const string authenticationScheme = JwtBearerDefaults.AuthenticationScheme;

            var connectionString = Configuration.GetConnectionString("Main");

            services.AddHttpContextAccessor();

            services.Configure<Settings>(Configuration);

            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = _ => false;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.Always;
            });

            var settings = Configuration.Get<Settings>();

            var key = Encoding.ASCII.GetBytes(settings.TokenSecret);

            services
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = authenticationScheme;
                    options.DefaultChallengeScheme = authenticationScheme;
                })
                .AddJwtBearer(x => {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDateTimeService, DateTimeService>();

            services.AddScoped<IRepository, SqlServerRepository>();

            services.AddControllersWithViews().AddNewtonsoftJson();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptionsMonitor<Settings> optionsMonitor)
        {
            Guard.AgainstNull(optionsMonitor, nameof(optionsMonitor));

            var settings = optionsMonitor.CurrentValue;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
