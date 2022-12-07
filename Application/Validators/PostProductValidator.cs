using System.Text.Json;
using Application.DTOs;
using Core.Entities;
using FluentValidation;
using SQLitePCL;

namespace Application.Validators;

public class PostProductValidator : AbstractValidator<ProductToCreateDTO>
{
    public PostProductValidator()
    {
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Name).NotEmpty();
    }
}

public class PostBrandValidator : AbstractValidator<ProductBrandToCreateDTO>
{
    public PostBrandValidator()
    {
      //  var badData = File.ReadAllText("../Application/Validators/Data/badData.json"); 
       // var badWods = JsonSerializer.Deserialize<List<string>>(badData);
        
         List<string> badwords = new List<string>();
         badwords.Add("one");
         badwords.Add("two");
         badwords.Add("three");
        
       RuleFor(p => p.Name).NotEmpty().WithMessage("please insert valid characters "); 
       RuleFor(p => p.Name).NotNull().WithMessage("name cant be null");
        foreach (var v in badwords)
        {
            RuleFor(p => p.Name).NotEqual(v);
        }
    }
}

public class PostTypeValidator : AbstractValidator<ProductTypeToCreateDTO>
{
    public PostTypeValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Id).GreaterThan(0);
    }
}

public class ProductTypeValidator : AbstractValidator<ProductType>
{
    public ProductTypeValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}

public class ProductBrandValidator : AbstractValidator<ProductBrand>
{
    public ProductBrandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}