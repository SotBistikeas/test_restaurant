﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.Extensions;
using FoodCost.Configuration;
using FoodCost.Identity;

using Abp.AspNetCore.SignalR.Hubs;
using FoodCost.Authorization.Users;
using Abp.IdentityServer4;
using FoodCost.Authentication.JwtBearer;

namespace FoodCost.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddMvc(
                options => options.Filters.Add(new CorsAuthorizationFilterFactory(_defaultCorsPolicyName))
            );

            IdentityRegistrar.Register(services);
            services.AddIdentityServer()
               .AddDeveloperSigningCredential()
               .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
               .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
               .AddInMemoryClients(IdentityServerConfig.GetClients())
               .AddAbpPersistedGrants<IAbpPersistedGrantDbContext>()
               .AddAbpIdentityServer<User>();
            services.AddAuthentication().AddIdentityServerAuthentication("IdentityBearer", options =>
                {
                    options.Authority = "http://localhost:21021/";
                    options.RequireHttpsMetadata = false;
                });

            AuthConfigurer.Configure(services, _appConfiguration);



            services.AddSignalR();

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "FoodCost API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            // Configure Abp and Dependency Injection
            return services.AddAbp<FoodCostWebHostModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware("IdentityBearer");
            app.UseIdentityServer();

            app.UseAbpRequestLocalization();


            app.UseSignalR(routes =>
            {
                routes.MapHub<AbpCommonHub>("/signalr");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_appConfiguration["App:ServerRootAddress"].EnsureEndsWith('/') + "swagger/v1/swagger.json", "FoodCost API V1");
                options.IndexStream = () => Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("FoodCost.Web.Host.wwwroot.swagger.ui.index.html");
            }); // URL: /swagger
        }
    }
}
