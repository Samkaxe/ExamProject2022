using System.ComponentModel.DataAnnotations;
using Application.DTOs;
using Application.Interfaces;
using Application.Mapper;
using Application.Models;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;
using ValidationException = FluentValidation.ValidationException;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IValidator<ProductToCreateDTO> _postValidator;
    private readonly IValidator<Product> _productValidator;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository repository,
        IMapper mapper, 
        IValidator<ProductToCreateDTO> postValidator,
        IValidator<Product> productValidator
        )
    {
        _repository = repository;
        _mapper = mapper;
        _postValidator = postValidator;
        _productValidator = productValidator;
    }

    public async Task<IReadOnlyList<Product>> GetAllProducts()
    {
        var products = await _repository.GetProductsAsync();
        
        return ObjectMapper.Mapper.Map<IReadOnlyList<Product>>(products);
    }
    
    public async Task<Product> GetProductById(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);
        return ObjectMapper.Mapper.Map<Product>(product);
    }

    public Product CreateNewProduct(ProductToCreateDTO dto)
    {
        try
        {
            //var product = _mapper.Map<ProductToCreateDTO, Product>(dto);

            var validation = _postValidator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException("line 32 method create new product in product service ");

            return _repository.CreateNewProduct(_mapper.Map<Product>(dto));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        throw new Exception();

    }
    
    public async Task<Product> UpdateProduct(Product product)
    {
        //
        // if (id != product.Id)
        //     throw new ValidationException("Method UpdateProduct line 47 is ProductService");
        //
        // var validation = _productValidator.Validate(product);
        // if (!validation.IsValid)
        //     throw new ValidationException(validation.ToString());
        //
        //
        // var dbproduct = await _repository.GetProductByIdAsync(id);
        
        return  _repository.UpdateProduct(product);
    }

    public Product DeleteProduct(int id)
    {
        return _repository.DeleteProduct(id);
    }
}