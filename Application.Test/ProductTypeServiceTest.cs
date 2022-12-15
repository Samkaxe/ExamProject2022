using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using static Xunit.Assert;

namespace Application.Test;

public class ProductTypeServiceTest
{
    private Mock<ITypeRepository> _repositoryMock;
    private ProductTypeService _service;
    private Mock<IValidator<ProductTypeToCreateDTO>> _createValidatorMock;
    private Mock<IMapper> _mapperMock;
    
    
    public ProductTypeServiceTest()
    {
        _createValidatorMock = new Mock<IValidator<ProductTypeToCreateDTO>>(MockBehavior.Default);
        _mapperMock = new Mock<IMapper>();
        _repositoryMock = new Mock<ITypeRepository>();
        _service = new ProductTypeService(_repositoryMock.Object ,_createValidatorMock.Object ,_mapperMock.Object);
        
    }

    [Fact]
    public void ShouldCallGetTypesAsyncFromRepositoryWhenCallingGetAllTypesMethod()
    {
        _service.GetAllTypes();
        _repositoryMock.Verify(repository => repository.GetTypesAsync(),Times.Once);
    }

    [Fact]
    public void ShouldCallGetProductTypeByIdAsyncWhenCallingGetTypeByIdMethod()
    {
        _service.GetTypeById(1);
        _repositoryMock.Verify(r => r.GetProductTypeByIdAsync(It.IsAny<int>()),Times.Once);
    }
    
    [Fact]
    public void ShouldCallDeleteBrandFromRepositoryWhenDeletingBrand()
    {
        _service.DeleteType(1);
        _repositoryMock.Verify(repository => repository.DeleteType(It.IsAny<int>()), Times.Once);
    }
    
    [Fact]
    public void ShouldThrowExceptionWhenInputDataIsNotValidForCreation()
    {
        var toCreateDto = new ProductTypeToCreateDTO()
        {
            Name = "testsd"
        };
        Assert.Throws<Exception>(() => _service.CreateType(toCreateDto));
    }

    [Fact]
    public void ShouldCallCreateNewBrandFromRepository()
    {
        var toCreateDto = new ProductTypeToCreateDTO()
        {
            Name = "testsd"
        };
        _createValidatorMock.Setup(validator => validator.Validate(It.IsAny<ProductTypeToCreateDTO>()))
            .Returns(() => new ValidationResult());
        _service.CreateType(toCreateDto);
        _repositoryMock.Verify(repository => repository.CreateType(It.IsAny<ProductType>()), Times.Once);
    }
    
    [Fact]
    public void ShouldCallDeleteProductFromRepositoryWhenDeletingProduct()
    {
        _service.DeleteType(1);
        _repositoryMock.Verify(repository => repository.DeleteType(It.IsAny<int>()), Times.Once);
    }
}