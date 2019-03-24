using System;
using System.IO;
using System.Reflection;
using BoardGamesServiceApi.BLL;
using BoardGamesServiceApi.DAL.EF.Context;
using BoardGamesServiceApi.DAL.Entities;
using BoardGamesServiceApi.DAL.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace BoardGamesServiceApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string boarGamesConnection = Configuration["Data:ConnectionStrings:BoardGamesConnection"];
            services.AddDbContext<BoardGameContext>(options => options.UseSqlServer(boarGamesConnection));

            services.AddScoped<IDbService, DbService>();

            services.AddSingleton(new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
            services.AddMemoryCache();

            services.AddResponseCaching();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation();

            services.AddSingleton<IValidator<BoardGame>, BoardGameValidator>();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

            });
        }

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

            app.UseResponseCaching();

            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue()
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromHours(1)
                    };
                context.Response.Headers[HeaderNames.Vary] = new string[] { "Accept-Encoding" };

                await next();
            });

            app.UseSwagger();

            app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });

            app.UseMvc();
        }
    }
}
