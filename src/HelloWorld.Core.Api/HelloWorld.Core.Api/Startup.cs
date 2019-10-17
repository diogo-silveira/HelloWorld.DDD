using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HelloWorld.Core.Api.Extensions;
using HelloWorld.Core.Api.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;


namespace HelloWorld.Core.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private IHostingEnvironment CurrentEnvironment { get; set; }
        public Startup( IHostingEnvironment env )
        {
            if (env == null) return;

            CurrentEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath( Directory.GetCurrentDirectory() )
                .AddJsonFile( "appsettings.json", true, true )
                .AddJsonFile( $"appsettings.{env.EnvironmentName}.json", true );

            if (builder != null)
            {
                builder.AddEnvironmentVariables();
                Configuration = builder.Build();
            }
        }

        public void ConfigureServices( IServiceCollection services )
        {
            if (services == null || Configuration == null) return;

            var policyWebSiteAllowed =
                    Configuration.GetSection( "CorsAllowSite:website" ).Value.Split( ';' ) != null
                    ? Configuration.GetSection( "CorsAllowSite:website" ).Value.Split( ';' )
                    : null;

            services.AddMvcCore()
                .AddJsonFormatters( options =>
                {
                    options.Formatting = Formatting.Indented;
                    options.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

                } ).AddApiExplorer();


            if (CurrentEnvironment.EnvironmentName != null && !CurrentEnvironment.EnvironmentName.Contains( "Development" ))
            {
                services.AddCors( options =>
                {
                    options.AddPolicy( "AllowPolicy",
                        policy => policy.WithOrigins( policyWebSiteAllowed )
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
                } );
            }
            else
            {
                services.AddCors( options =>
                {
                    options.AddPolicy( "AllowPolicy",
                        policy => policy.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                    );
                } );
            }


            services.AddMvc();

            services.AddSwaggerGen( s =>
            {
                s.SwaggerDoc( "v1", new Info
                {
                    Version = "v1",
                    Title = "API Core",
                    Description = "API Swagger",
                    Contact = new Contact
                    {
                        Name = "Diogo Silveira",
                        Email = "fraga.diogo@gmail.com"
                    }
                } );
            } );

            Runtime.Issuer = Configuration.GetSection( "TokenAuthentication:Issuer" ).Value != null
                ? Configuration.GetSection( "TokenAuthentication:Issuer" ).Value
                : string.Empty;
            Runtime.Audience = Configuration.GetSection( "TokenAuthentication:Audience" ).Value != null
                ? Configuration.GetSection( "TokenAuthentication:Audience" ).Value
                : string.Empty;
            Runtime.SecurityKey = Configuration.GetSection( "TokenAuthentication:SecurityKey" ).Value != null
                ? Configuration.GetSection( "TokenAuthentication:SecurityKey" ).Value
                : string.Empty;

            services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddCookie( options =>
                {
                    options.AccessDeniedPath = "/";
                    options.LoginPath = "/";
                } )
                .AddJwtBearer( options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Runtime.Issuer,
                        ValidAudience = Runtime.Audience,
                        IssuerSigningKey = JwtSecurityKey.Create( Runtime.SecurityKey )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine( "OnAuthenticationFailed: " + context.Exception.Message );
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine( "OnTokenValidated: " + context.SecurityToken );
                            return Task.CompletedTask;
                        }
                    };
                } );

            services.Configure<RouteOptions>( options => options.LowercaseUrls = true );

            services.AddDependencyInjection();
        }

        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            if (app == null) throw new ArgumentNullException( nameof( app ) );
            if (env == null) throw new ArgumentNullException( nameof( env ) );
            if (loggerFactory == null) throw new ArgumentNullException( nameof( loggerFactory ) );

            if (Configuration != null) loggerFactory.AddConsole( Configuration.GetSection( "Logging" ) );


            if (env.IsStaging() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();

                app.UseCors( "AllowPolicy" );
            }
            else if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseBrowserLink();

                app.UseCors( "AllowPolicy" );
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI( s =>
            {
                s.SwaggerEndpoint( "../swagger/v1/swagger.json", "Serko Core Project API v1.0" );
            } );
        }
    }
}
