using API.Extensions;
using API.Mapper;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Data;
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
            services.AddApplicationServices();
            
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_configuration.GetConnectionString("Default")));
            
             services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
             });

             services.AddSingleton<IConnectionMultiplexer>(c =>
             {
                 var config = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"),
                     true);
                 return ConnectionMultiplexer.Connect(config);
             });

             // services.AddCors(opt => {
             //     opt.AddPolicy("CorsPolicy", policy => {
             //         policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
             //     });
             // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
