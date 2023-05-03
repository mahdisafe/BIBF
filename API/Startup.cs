using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace API
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
            services.AddScoped<IProductRespository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof (GenericRepository<>)));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddDbContext<StoreContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
 
          
           services.AddControllers();
           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

         app.UseMiddleware<ExceptionMiddleware>();

  



            if (env.IsDevelopment())
            {
              

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));

                
            }
          
  
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
              
        }
    }
}
