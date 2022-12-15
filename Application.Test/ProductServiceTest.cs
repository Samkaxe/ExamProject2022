using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Test;

public class ProductServiceTest
{
    private ProductService _service;
    private Mock<IProductRepository> _repositoryMock;
    private Mock<IValidator<ProductToCreateDTO>> _createValidatorMock;
    private Mock<IValidator<Product>> _productValidatorMock;
    private Mock<IMapper> _mapperMock;

    public ProductServiceTest()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _createValidatorMock = new Mock<IValidator<ProductToCreateDTO>>(MockBehavior.Default);
        _mapperMock = new Mock<IMapper>();
        _productValidatorMock = new Mock<IValidator<Product>>(MockBehavior.Default);
        _service = new ProductService(_repositoryMock.Object, _mapperMock.Object, _createValidatorMock.Object,
            _productValidatorMock.Object);
    }

    [Fact]
    public void ShouldCallGetProductsAsyncWhenGettingAllProducts()
    {
        _service.GetAllProducts();
        _repositoryMock.Verify(repository => repository.GetProductsAsync(), Times.Once());
    }

    [Fact]
    public void ShouldCallGetProductByIdAsyncWhenGettingProductById()
    {
        _service.GetProductById(1);
        _repositoryMock.Verify(repository => repository.GetProductByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void ShouldCallDeleteProductFromRepositoryWhenDeletingProduct()
    {
        _service.DeleteProduct(1);
        _repositoryMock.Verify(repository => repository.DeleteProduct(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void ShouldThrowExceptionWhenInputDataIsNotValidForCreation()
    {
        var toCreateDto = new ProductToCreateDTO
        {
            Description = "description", Price = 0, PictureUrl = "", ProductBrandId = 0,
            ProductTypeId = 0
        };
        Assert.Throws<Exception>(() => _service.CreateNewProduct(toCreateDto));
    }

    [Fact]
    public void ShouldCallCreateNewProductFromRepository()
    {
        var toCreateDto = new ProductToCreateDTO
        {
            Description = "description", Price = (decimal) 20.30, PictureUrl = "", ProductBrandId = 1,
            ProductTypeId = 1, Name = "name"
        };
        _createValidatorMock.Setup(validator => validator.Validate(It.IsAny<ProductToCreateDTO>()))
            .Returns(() => new ValidationResult());
        _service.CreateNewProduct(toCreateDto);
        _repositoryMock.Verify(repository => repository.CreateNewProduct(It.IsAny<Product>()), Times.Once);
    }
    
    
    [Fact]
    public async Task ShouldThrowExceptionWhenInputProductIsInvalid()
    {
        _productValidatorMock.Setup(validator => validator.Validate(It.IsAny<Product>()))
            .Returns(new ValidationResult(new[] {new ValidationFailure("Name", "required")}));
        await Assert.ThrowsAsync<ValidationException>(() => _service.UpdateProduct( new Product {Id = 1}));
    }

    [Fact]
    public void ShouldCallUpdateProductFromRepositoryWhenUpdating()
    {
        _productValidatorMock.Setup(validator => validator.Validate(It.IsAny<Product>()))
            .Returns(new ValidationResult());
        _service.UpdateProduct(
            new Product
            {
                Id = 1, Name = "name", Description = "description", Price = (decimal) 20.30, PictureUrl = "",
                ProductBrandId = 1, ProductTypeId = 1
            });
        _repositoryMock.Verify(repository => repository.UpdateProduct(It.IsAny<Product>()),Times.Once);
    }
    
    // [Fact]
    // public void  shouldDeleteproduct()
    // {        
    //   
    //     var id = 1;
    //     _repositoryMock.Setup(repo => repo.GetProductByIdAsync(id)).ReturnsAsync(new Product(){});
    //     _repositoryMock.Setup(repo => repo.DeleteProduct(It.IsAny<Product>())).Returns(Task.CompletedTask);
    //
    //    
    //      _service.DeleteProduct(id);
    //
    //     
    //     _repositoryMock.Verify(repo => repo.DeleteProduct(It.IsAny<Product>()),Times.Once);
    // }
}