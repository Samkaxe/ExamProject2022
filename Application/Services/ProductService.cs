using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.Data;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly ProductRepository _repository;
    private readonly IValidator<ProductToCreateDTO> _postValidator;
    private readonly IValidator<Product> _productValidator;
    private readonly IMapper _mapper;

    public ProductService(ProductRepository repository,IValidator<ProductToCreateDTO> postValidator , IValidator<Product> productValidator, IMapper mapper )
    {
        _repository = repository;
        _mapper = mapper;
        _postValidator = postValidator;
        _productValidator = productValidator;
    }

    public async Task<IReadOnlyList<Product>> GetAllProducts()
    {
        return await _repository.GetProductsAsync();
    }

    public Product CreateNewProduct(ProductToCreateDTO dto)
    {
        var validation = _postValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());

        return _repository.CreateNewProduct(_mapper.Map<Product>(dto));
        
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _repository.GetProductByIdAsync(id);
    }

    public Product UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            throw new ValidationException("Method UpdateProduct line 47 is ProductService");
        
        var validation = _productValidator.Validate(product);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        return _repository.UpdateProduct(product);
    }

    public Product DeleteProduct(int id)
    {
        return _repository.DeleteProduct(id);
    }
}