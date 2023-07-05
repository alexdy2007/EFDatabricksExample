using Microsoft.EntityFrameworkCore;
using EFBricks.Service.DatabricksAPI;
using EFBricks.API.Database;
using Newtonsoft.Json;

namespace EFBricks
{
    public class Startup
    {

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>{
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("http://localhost:3000", "http://localhost:3001")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );                //.AllowCredentials());
            });

            services
            .AddMvc(option => option.EnableEndpointRouting = false)
            .AddNewtonsoftJson(o => {o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;});
           

            services.AddScoped<IDatabricksApiService, DatabricksApiService>();

        
            services.AddDbContext<EFBricksDbContext>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicy");   

            }
            else{
                app.UseExceptionHandler();
            }

            app.UseMvc();
            app.UseStatusCodePages();

        }
    }
}