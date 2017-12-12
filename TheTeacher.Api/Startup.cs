using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TheTeacher.Infrastructure.Mapper;
using TheTeacher.Infrastructure.Repositories;
using TheTeacher.Infrastructure.Services;
using TheTeacher.Infrastructure.IoC;
using TheTeacher.Infrastructure.Settings;
using TheTeacher.Infrastructure.Extensions;
using System.Text;

namespace TheTeacher.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public JwtSettings JwtSettings { get; private set; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc()
                    .AddJsonOptions( jsonOpt => jsonOpt.SerializerSettings.Formatting = Formatting.Indented);
            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer( opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = JwtSettings.Issuer,
                            ValidateAudience = false,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key))
                        };
                    });

            // Autofac setup
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            JwtSettings = app.ApplicationServices.GetService<JwtSettings>();
            var generalSettings  = app.ApplicationServices.GetService<GeneralSettings>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            if(generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }

            app.UseMvc();
            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());

        }
    }
}
