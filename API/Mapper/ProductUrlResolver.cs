using Application.DTOs;
using Application.Models;
using AutoMapper;
using Core.Entities;

namespace API.Mapper;

public class ProductUrlResolver :IValueResolver<Product , ProductToReturnDto , string>
{
    private readonly IConfiguration _config;

    public ProductUrlResolver(IConfiguration config)
    {
        _config = config;
    }

    // this class was created to solve the issue with the pictureUrl that cant be send via the client 
    public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {  return _config["ApiUrl"] + source.PictureUrl ;}


        return null;
    }
}