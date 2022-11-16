using Application.DTOs;
using Application.Models;
using AutoMapper;
using Core.Entities;

namespace API.Mapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ProductModel, ProductToReturnDto>()
            .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        
        
        CreateMap<ProductToCreateDTO, Product>();
        
    } 
}