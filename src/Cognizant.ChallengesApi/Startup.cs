using Autofac;
using Autofac.Configuration;
using Cognizant.ChallangesApi.Diagnostic.Health;
using Cognizant.ChallangesApi.Filters;
using Cognizant.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Cognizant.ChallangesApi
{
    public class Startup
    {
        private IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            Environment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddHostedService<Readiness>();
            services.AddSingleton<StartupHostedServiceHealthCheck>();

            services.AddHealthChecks()
                .AddCheck<HealthCheck>(
                    nameof(HealthCheck),
                    HealthStatus.Degraded,
                    tags: new[] { "health" })
                .AddCheck<StartupHostedServiceHealthCheck>(
                    nameof(StartupHostedServiceHealthCheck),
                    HealthStatus.Degraded,
                    tags: new[] { "ready" });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            if (Convert.ToBoolean(Configuration["Swagger:Enabled"]))
            {
                services.AddSwaggerGen(options =>
                {
                    options.IncludeXmlComments(
                        Path.ChangeExtension(
                            typeof(Startup).Assembly.Location,
                            "xml"));

                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = Configuration["App:Title"],
                        Version = Configuration["App:Version"],
                        Description = Configuration["App:Description"],
                        Contact = new OpenApiContact
                        {
                            Name = Configuration["App:Contact:Name"],
                            Email = Configuration["App:Contact:Email"],
                            Url = new Uri(Configuration["App:Contact:Url"])
                        }
                    });

                    options.CustomSchemaIds(x => x.FullName);
                });
            }

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "CognizantChallengeApp/dist";
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ConfigurationModule(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("health"),
                    ResponseWriter = (context, result) =>
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        return context.Response.WriteAsync(result.Status == HealthStatus.Healthy ? "GOOD" : "NOT GOOD");
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status404NotFound
                    }
                });

                endpoints.MapHealthChecks("/readiness", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                    ResponseWriter = (context, result) =>
                    {
                        context.Response.ContentType = "application/json; charset=utf-8";
                        return context.Response.WriteAsync(result.Status == HealthStatus.Healthy ? "GOOD" : "NOT GOOD");
                    },
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "/");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "CognizantChallengeApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            if (!env.IsProduction() && Convert.ToBoolean(Configuration["Swagger:Enabled"]))
            {
                app.UseSwagger()
                   .UseSwaggerUI(c =>
                   {
                       c.SwaggerEndpoint("/swagger/v1/swagger.json", Configuration["App:Title"]);
                   });
            }
        }
    }
}
