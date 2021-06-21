using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCoreApiWithEF.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using interfaces;
using implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using EntityLib;

namespace DotnetCoreApiWithEF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200/").AllowAnyOrigin().AllowAnyHeader()
                                .AllowAnyMethod(); 
                });
            });
            services.AddTransient<IEmployee, employee>();

            services.AddTransient<Func<string, interfaceType>>((provider) => {
                return new Func<string, interfaceType>((targetString) => new implement(targetString));
                });
            
            services.AddTransient<Func<string,int, interfaceType>>((provider) => {
                return new Func<string,int, interfaceType>((targetString,tragetInt) => new implement(targetString, tragetInt));
            });
            //var a = Configuration.GetConnectionString("TestConnection");
            services.AddDbContext<EFDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("TestConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)  
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseMvc();
           

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
