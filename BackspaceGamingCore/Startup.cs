using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BackspaceGaming.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using URF.Core.Abstractions;
using URF.Core.EF;

namespace BackspaceGamingCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var origins = Configuration["Origins"].Split(';').ToArray();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(origins);
                }));

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["ConnectionString:SqlConnection"]));
            services.AddTransient(provider => Configuration);

   
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
   
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
                    "text/plain",
                    "text/css",
                    "application/javascript",
                    "text/html",
                    "application/xml",
                    "text/xml",
                    "application/json",
                    "text/json",
                    "image/svg+xml"
                };
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Service_base"],
                        ValidAudience = Configuration["Origins"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt_Key"])),
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,
                        ClockSkew = TimeSpan.Zero

                    };
                });

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<DatabaseContext>().As<DbContext>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            Assembly repository = Assembly.Load("BackspaceGaming.Repository");
            Assembly service = Assembly.Load("BackspaceGaming.Service");
   
            containerBuilder.RegisterAssemblyTypes(repository)
                           .AsImplementedInterfaces();
            containerBuilder.RegisterAssemblyTypes(service)
                         .AsImplementedInterfaces();

            containerBuilder.Populate(services);

            var container = containerBuilder.Build();

           
            return new AutofacServiceProvider(container);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseMvc();

            app.Use(async (context, next) =>
            {
                await next();
            });

            app.Run(async context =>
            {

                if (context.Request.Path.Value == "/")
                {
                    string ver = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
                    await context.Response.WriteAsync($"{{status :'API is Running.'}} Version: {ver}");
                }
                else
                {
                    await context.Response.WriteAsync("{ status :'404 Method Not Found'}");
                }
            });

            
           
        }
    }
}
