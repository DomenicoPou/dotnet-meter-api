using dotnet_meter_api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using dotnet_meter_api.Handlers;
using System.IO;
using NLog;
using NLog.Config;

namespace dotnet_meter_api
{
    public class Startup
    {
        private Logger logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure NLog
            ConfigureNLog();

            try
            {
                services.AddControllers();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = "Meter Readings API",
                            Version = ObtainVersion()
                        }); ;
                });

                var connectionString = Configuration.GetConnectionString($"DatabaseConnection-{EnvironmentHandler.Get()}");
                services.AddDbContext<DatabaseContext>(options => options
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            } catch (Exception ex)
            {
                logger.Error($"An error occured when adding services configurations: {ex} \r\n {ex.StackTrace}");
                throw ex;
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnet_meter_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// Obtain the current version of the api
        /// </summary>
        /// <returns>The version of the projects assebly</returns>
        private string ObtainVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            return fvi.FileVersion;
        }

        /// <summary>
        /// Configures the NLog Package
        /// </summary>
        private void ConfigureNLog()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(assemblyFolder, "Config", $"NLog-{EnvironmentHandler.Get()}.config"), true);
            logger = LogManager.GetCurrentClassLogger();
        }
    }
}
