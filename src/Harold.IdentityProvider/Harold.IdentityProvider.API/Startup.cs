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
            services.AddMvc()
                    .AddFluentValidation()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSingleton(Configuration);
            services.AddDbContext<HaroldIdentityProviderContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IValidator<Roles>, RolesValidator>();
            services.AddTransient<IValidator<UsersRequest>, UsersRequestValidator>();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
