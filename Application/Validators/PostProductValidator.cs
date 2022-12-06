using Application.DTOs;
using Core.Entities;
using FluentValidation;

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
        RuleFor(p => p.Name).NotEmpty();
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