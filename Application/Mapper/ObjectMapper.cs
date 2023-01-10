using Application.DTOs;
using Application.Models;
using AutoMapper;
using Core.Entities;

namespace Application.Mapper;
//Provides a named configuration for maps. Naming conventions become scoped per profile.
public static class ObjectMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<DtoMapper>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });

    public static IMapper Mapper => Lazy.Value;
}

public class DtoMapper : Profile
{
    public DtoMapper()
    {
        CreateMap<Product, ProductModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
      
           
        CreateMap<ProductBrand, ProductBrandModel>().ReverseMap();
        CreateMap<ProductType, ProductTypeModel>().ReverseMap();
    } 
}