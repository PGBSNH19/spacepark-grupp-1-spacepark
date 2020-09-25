using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SpaceParkAPI.Repos;
using SpaceParkAPI.Db_Context;

namespace SpaceParkAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowFrontEnd",
                    builder =>
                    {
                        builder.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            //services.AddCors();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddControllers();
            services.AddScoped<IPersonRepo,PersonRepo>();
            services.AddScoped<ISpaceshipRepo, SpaceshipRepo>();
            services.AddDbContext<SpaceParkContext>();
            services.AddScoped<IParkingSpaceRepo, ParkingSpaceRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //app.UseCors(
            //    options => options.WithOrigins("http://127.0.0.1:5500")
            //    .AllowAnyMethod()
            //    );

            //app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
