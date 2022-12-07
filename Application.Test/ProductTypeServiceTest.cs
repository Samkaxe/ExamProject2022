using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
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
}