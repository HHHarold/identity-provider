﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Harold.IdentityProvider.API.Filters;
using Harold.IdentityProvider.Model.FluentValidators;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Repository;
using Harold.IdentityProvider.Repository.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;


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
            services.AddMvc(opt =>
                        {
                            opt.Filters.Add(typeof(ValidatorActionFilter));
                        })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddFluentValidation();
            services.AddSingleton(Configuration);
            services.AddDbContext<HaroldIdentityProviderContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IValidator<Roles>, RolesValidator>();
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
