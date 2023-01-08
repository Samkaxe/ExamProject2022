using API.Errors;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

public static class ApplicatonServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AddTransient have short lifetime call then destroy the requsit 
        // addSingleton repo start when the app start but never be desroyed even when the app shotdown 
        // add scope  best option so far called based on the http call 
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        /*
         * AddTransient
         Transient lifetime services are created each time they are requested.
          This lifetime works best for lightweight, stateless services.
        AddScoped
        Scoped lifetime services are created once per request.
        AddSingleton
        Singleton lifetime services are created the first time they are requested 
        (or when ConfigureServices is run if you specify an instance there)
         and then every subsequent request will use the same instance.
         */
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IBrandService, ProductBrandService>();
        
        services.AddScoped<ITypeRepository, TypeRepository>();
        services.AddScoped<ITypeService, ProductTypeService>();
        
        services.AddTransient<IUploadService, UploadService>();
        
        // override the behavior of the controller attribute 
        // one or more validations acoure so it help me to find where is the error 
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}