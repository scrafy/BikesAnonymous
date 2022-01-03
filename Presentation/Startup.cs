using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraversalServices.Setup;
using DataLayer.Setup;
using OwnerCMD.Setup;
using System;
using BikesAnonymous.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TraversalServices.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using BikesAnonymous.Models;
using Core.Exceptions;
using Core.Enums;
using Newtonsoft.Json;
using System.IO;

namespace BikesAnonymous
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

            LoadConfigurationSections(services);
            RegisterProviderServices(services);
            JWTConfiguration(services);
            SetupCorsPolicy(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(ManageException);

            registerMiddleWares(app);            

          
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseSwagger();
          //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BikesAnonymous API v1.0"));

        }

        private void registerMiddleWares(IApplicationBuilder app)
        {
            app.UseMiddleware<UnauthorizedResponseMiddleware>();
        }

        private void RegisterProviderServices(IServiceCollection services)
        {
            services.AddControllers();
            services.RegisterInfraestructureServices(Configuration);
            services.RegisterDataLayerRepositories();
            services.RegisterOwnerCommandsProvider();
            services.RegisterCyclistCommandsProvider();
        }

        private void LoadConfigurationSections(IServiceCollection services)
        {
            services.LoadInfraestructureConfigurationSections(Configuration);
                
        }

        private void JWTConfiguration(IServiceCollection services)
        {
            var tokenSettingsSection = Configuration.GetSection("TokenSettings");
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.RequireHttpsMetadata = false;
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Secret)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ClockSkew = TimeSpan.Zero
               };
           });
            
        }

        private void SetupCorsPolicy(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); 

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
        }

        private void ManageException(IApplicationBuilder appBuilder)
        {
            appBuilder.Run(async context =>
            {

                var ex = context.Features.Get<IExceptionHandlerFeature>().Error;
                new GlobalHandlerException().HandleException(ex, context);

            });
        }
    }
}
