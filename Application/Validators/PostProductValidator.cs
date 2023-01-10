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
      // this.RuleFor('price').matches(new RegExp('^([0-9])+.([0-9]){2}$'));
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Description).NotEmpty().MaximumLength(500);
        RuleFor(p => p.ProductBrandId).NotNull().WithMessage("you must select Brand");
        RuleFor(p => p.ProductTypeId).NotNull().WithMessage("you must select Type");
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
       // we thought we could validate bad words dictionary by sets of string in json file but we found out json hold object 
       // so the best way was normal file, deserialize to to list and with in loop check for validation 
       // it works as you can see but we didnot give it too much attention since if we use real file its gonna be big as i think and this might take some memory
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
        RuleFor(p => p.Price).GreaterThan(0).NotNull();
        RuleFor(p => p.Name).NotEmpty().MaximumLength(250);
        RuleFor(p => p.Id).GreaterThan(0).NotNull();
        RuleFor(p => p.Description).NotEmpty().NotNull();
        RuleFor(p => p.PictureUrl).NotEmpty().NotNull();
        RuleFor(p => p.ProductType).NotEmpty().NotNull();
        RuleFor(p => p.ProductBrand).NotNull();
        RuleFor(p => p.ProductType).NotNull();
    }
}

public class ProductTypeValidator : AbstractValidator<ProductType>
{
    public ProductTypeValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(P => P.Id).NotNull();
    }
}

public class ProductBrandValidator : AbstractValidator<ProductBrand>
{
    public ProductBrandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(P => P.Id).NotNull();
    }
}