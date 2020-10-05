using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpaceParkAPI.Repos;
using SpaceParkAPI.Db_Context;
using Serilog;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace SpaceParkAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
            .WriteTo.MSSqlServer
            (
                connectionString: Configuration.GetConnectionString("DefaultConnection"),
                sinkOptions: new SinkOptions { TableName = "EventLogs", AutoCreateSqlTable = true }
            )
            .WriteTo.Debug()
            .CreateLogger();
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
                        builder.WithOrigins("http://127.0.0.1:5500",
                                            "https://frontendwebsw.azurewebsites.net/")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddScoped<IPersonRepo,PersonRepo>();
            services.AddScoped<ISpaceshipRepo, SpaceshipRepo>();
            services.AddDbContext<SpaceParkContext>();
            services.AddScoped<IParkingSpaceRepo, ParkingSpaceRepo>();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }          

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
