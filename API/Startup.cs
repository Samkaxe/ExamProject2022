using API.Extensions;
using API.Mapper;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
           
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers();
            services.AddApplicationServices();
            services.AddIdentityServices(_configuration);
            
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_configuration.GetConnectionString("Default")));
           
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlite(_configuration.GetConnectionString("Identity"));
            });
            
            services.AddSwaggerDocs();
             // services.AddSwaggerGen(c =>
             // {
             //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
             // });
            
             services.AddSingleton<IConnectionMultiplexer>(c => 
                 //this is the object that hides away the details of multiple servers
                 // it is designed to be shared and reused between callers
                 //It is fully thread-safe and ready for this usage
                 // ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
             {
                 // setup the connection string of ridis , parse it 
                 //"Redis" : "localhost"
                 var config = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"),
                     true);
                 return ConnectionMultiplexer.Connect(config);
             });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                 app.UseDeveloperExceptionPage();
                 app.UseSwagger();
                 app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();
            
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseRouting();
            app.UseStaticFiles();
            //app.UseCors("CorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
