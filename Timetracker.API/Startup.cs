using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hemophilia.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Timetracker.Core.Domain.Interface;
using Timetracker.Core.Infrastructure;
using Timetracker.Core.Infrastructure.Data;
using TimeTracker.Core.Domain.Interface;
using TimeTracker.Core.Infrastructure.Data;

namespace Timetracker.API
{
    public class Startup
    {
        public IHostingEnvironment Environment { get; }
        public Startup(IHostingEnvironment env)
        {
            Environment = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterAuthenticationServices(services);
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<TimeTrackerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TimeTrackerConnection")));
            RegisterMvcServices(services);
            services.AddScoped<IRepository, EFRepository>();
            services.AddScoped<IReadOnlyRepositry, EFReadOnlyRepository>();
            services.AddScoped<IEffortRepository, EffortRepository>();
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
            app.UseAuthentication();
            app.UseCors("AllowAllHeaders");
            app.UseMvc();

        }
        public virtual void RegisterAuthenticationServices(IServiceCollection services)
        {
            //JWT bearer authentication for the API
            services.AddAuthentication(sharedOptions =>
                {
                    sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddAzureAdBearer(options => Configuration.Bind("AzureAd", options));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();

                    });
            }
            );
        }

        public virtual void RegisterMvcServices(IServiceCollection services)
        {

            services.AddMvc(x =>
            {
                if (Environment.IsDevelopment())
                    x.Filters.Add(new AllowAnonymousFilter());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
