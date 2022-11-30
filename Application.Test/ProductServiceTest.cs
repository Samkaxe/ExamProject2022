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
    private Mock<IProductRepository> _repositoryMock; //to mock IProductRepository interface
    private Mock<IValidator<ProductToCreateDTO>> _createValidatorMock; //to mock IValidator interface
    private Mock<IValidator<Product>> _productValidatorMock; //to mock IValidator<Product> interface
    private Mock<IMapper> _mapperMock; //to mock mapper

    public ProductServiceTest()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _createValidatorMock = new Mock<IValidator<ProductToCreateDTO>>(MockBehavior.Default);
        _mapperMock = new Mock<IMapper>();
        _productValidatorMock = new Mock<IValidator<Product>>(MockBehavior.Default);
        _service = new ProductService(_repositoryMock.Object, _mapperMock.Object, _createValidatorMock.Object,
            _productValidatorMock.Object); //creating new object from service class and using mock objects for constructor inputs
    }

    [Fact]
    public void ShouldCallGetProductsAsyncWhenGettingAllProducts()
    {
        _service.GetAllProducts(); //call service method
        _repositoryMock.Verify(repository => repository.GetProductsAsync(), Times.Once());
        // if GetProductsAsync method has been called only once from repository interface then test will pass.
    }

    [Fact]
    public void ShouldCallGetProductByIdAsyncWhenGettingProductById()
    {
        _service.GetProductById(1); //call service method
        _repositoryMock.Verify(repository => repository.GetProductByIdAsync(It.IsAny<int>()), Times.Once);
        // if GetProductByIdAsync method has been called only one time from repository interface then test will pass.
    }

    [Fact]
    public void ShouldCallDeleteProductFromRepositoryWhenDeletingProduct()
    {
        _service.DeleteProduct(1); //call service method
        _repositoryMock.Verify(repository => repository.DeleteProduct(It.IsAny<int>()), Times.Once);
        // if DeleteProduct method has been called only one time from repository interface then test will pass.
    }

    [Fact]
    public void ShouldThrowExceptionWhenInputDataIsNotValidForCreation()
    {
        var toCreateDto = new ProductToCreateDTO        //sample invalid data data
        {
            Description = "description", Price = 0, PictureUrl = "", ProductBrandId = 0,
            ProductTypeId = 0
        };
        Assert.Throws<Exception>(() => _service.CreateNewProduct(toCreateDto));
        //because of invalid data this test should throw exception to pass and if does not throw exception test will fail.
    }

    [Fact]
    public void ShouldCallCreateNewProductFromRepository()
    {
        var toCreateDto = new ProductToCreateDTO //sample data
        {
            Description = "description", Price = (decimal) 20.30, PictureUrl = "", ProductBrandId = 1,
            ProductTypeId = 1, Name = "name"
        };
        _createValidatorMock.Setup(validator => validator.Validate(It.IsAny<ProductToCreateDTO>()))
            .Returns(() => new ValidationResult());  //make validator pass data as vadid value
        _service.CreateNewProduct(toCreateDto);     //call service method
        _repositoryMock.Verify(repository => repository.CreateNewProduct(It.IsAny<Product>()), Times.Once);
        // if CreateNewProduct method has been called only one time from repository interface then test will pass.
    }

    [Fact]
    public void ShouldThrowExceptionWhenInputIdIsNotEqualToProductId()
    {
        Assert.Throws<ValidationException>(() => _service.UpdateProduct(1, new Product {Id = 2}));
        //this test should throw exception to pass because id and productId are not equal
    }

    [Fact]
    public void ShouldThrowExceptionWhenInputProductIsInvalid()
    {
        _productValidatorMock.Setup(validator => validator.Validate(It.IsAny<Product>()))
            .Returns(new ValidationResult(new[] {new ValidationFailure("Name", "required")})); //setup validation method for required name
        Assert.Throws<ValidationException>(() => _service.UpdateProduct(1, new Product {Id = 1})); //verify test to throw exception
    }

    [Fact]
    public void ShouldCallUpdateProductFromRepositoryWhenUpdating()
    {
        _productValidatorMock.Setup(validator => validator.Validate(It.IsAny<Product>()))
            .Returns(new ValidationResult());//setup validation method to pass test
        _service.UpdateProduct(1,
            new Product
            {
                Id = 1, Name = "name", Description = "description", Price = (decimal) 20.30, PictureUrl = "",
                ProductBrandId = 1, ProductTypeId = 1
            });  //sample data to update
        _repositoryMock.Verify(repository => repository.UpdateProduct(It.IsAny<Product>()), Times.Once);
        // if UpdateProduct method has been called only one time from repository interface then test will pass.
    }
}