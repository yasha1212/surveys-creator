using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITechArt.SurveysCreator.DAL;
using ITechArt.SurveysCreator.Foundation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ITechArt.SurveysCreator.WebApp
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            AppConfiguration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEntityService, UserService>();
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(AppConfiguration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                logger.LogInformation($"{context.Request.Path} request is being processed");
                await next.Invoke();
            });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
