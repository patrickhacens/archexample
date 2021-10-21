using ArchExample.Infrastructure.Behaviors;
using ArchExample.Infrastructure.Retornator;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudes.Retornator.AspnetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArchExample
{
    public class Startup
    {
        private readonly IHostEnvironment environment;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            services.AddMediatR(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            AssemblyScanner.FindValidatorsInAssembly(assembly)
                .ForEach(d => services.AddSingleton(d.InterfaceType, d.ValidatorType));


            services.AddResponseManager<ArchExampleResponseManager>();


            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    ConfigureJsonOptions(options.JsonSerializerOptions);
                })
                .AddRetornator(options =>
                {
                    ConfigureJsonOptions(options);
                });

            services.AddDbContext<Db>(options => options.UseInMemoryDatabase("ArchExample"));
        }

        private void ConfigureJsonOptions(JsonSerializerOptions options)
        {
            options.PropertyNameCaseInsensitive = true;
            options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.WriteIndented = environment.IsDevelopment();
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
