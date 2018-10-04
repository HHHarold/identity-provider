using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Repository;
using Harold.IdentityProvider.Repository.SqlServer;
using Harold.IdentityProvider.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Harold.IdentityProvider.Model.FluentValidators;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;
using Harold.IdentityProvider.API.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace Harold.IdentityProvider.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("AppSettings.json")
                            .AddJsonFile($"AppSettings.{env.EnvironmentName}.json", optional: true).AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddDbContext<HaroldIdentityProviderContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddMvc()
                    .AddFluentValidation()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSingleton(Configuration);
                        
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                   OnTokenValidated = context =>
                   {
                       var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
                       var userId = int.Parse(context.Principal.Identity.Name);
                       var user = unitOfWork.Users.GetById(userId);
                       if (user == null) context.Fail("Unauthorized");
                       return Task.CompletedTask;
                   }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IValidator<Roles>, RolesValidator>();
            services.AddTransient<IValidator<UsersRequest>, UsersRequestValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
