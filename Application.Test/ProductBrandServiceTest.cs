using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Application.Test;

public class ProductBrandServiceTest
{
    private Mock<IBrandRepository> _repositoryMock;
    private ProductBrandService _service;
    private Mock<IValidator<ProductBrandToCreateDTO>> _createValidatorMock;
    private Mock<IMapper> _mapperMock;

    public ProductBrandServiceTest()
    {
        _createValidatorMock = new Mock<IValidator<ProductBrandToCreateDTO>>(MockBehavior.Default);
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<IBrandRepository>();
        _service = new ProductBrandService(_repositoryMock.Object , _createValidatorMock.Object , _mapperMock.Object);
       
    }
    

    [Fact]
    public void ShouldCallGetBrandsAsyncFromRepositoryToGetAllBrands()
    {
        _service.GetAllBrands();
        _repositoryMock.Verify(repository => repository.GetBrandsAsync(), Times.Once);
    }

    [Fact]
    public void ShouldCallGetProductBrandByIdAsyncToGetBrandById()
    {
        _service.GetProductBrandById(1);
        _repositoryMock.Verify(repository => repository.GetProductBrandByIdAsync(It.IsAny<int>()), Times.Once());
    }
    
    [Fact]
    public void ShouldCallDeleteBrandFromRepositoryWhenDeletingBrand()
    {
        _service.DeleteBrand(1);
        _repositoryMock.Verify(repository => repository.DeleteBrand(It.IsAny<int>()), Times.Once);
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenInputDataIsNotValidForCreation()
    {
        var toCreateDto = new ProductBrandToCreateDTO
        {
            Name = "testsd"
        };
        Assert.Throws<Exception>(() => _service.CreateBrand(toCreateDto));
    }

    [Fact]
    public void ShouldCallCreateNewBrandFromRepository()
    {
        var toCreateDto = new ProductBrandToCreateDTO
        {
            Name = "testsd"
        };
        _createValidatorMock.Setup(validator => validator.Validate(It.IsAny<ProductBrandToCreateDTO>()))
            .Returns(() => new ValidationResult());
        _service.CreateBrand(toCreateDto);
        _repositoryMock.Verify(repository => repository.CreateBrand(It.IsAny<ProductBrand>()), Times.Once);
    }
}