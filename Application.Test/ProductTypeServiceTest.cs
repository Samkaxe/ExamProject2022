using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Moq;
using static Xunit.Assert;

namespace Application.Test;

public class ProductTypeServiceTest
{
    private Mock<ITypeRepository> _repositoryMock; //to mock ITypeRepository instead of implement it
    private ProductTypeService _service;

    public ProductTypeServiceTest()
    {
        _repositoryMock = new Mock<ITypeRepository>();   //creating new mock
        _service = new ProductTypeService(_repositoryMock.Object);
    }

    [Fact]
    public void ShouldCallGetTypesAsyncFromRepositoryWhenCallingGetAllTypesMethod()
    {
        _service.GetAllTypes();  //call  GetAllTypes from service class to test and verify
        _repositoryMock.Verify(repository => repository.GetTypesAsync(),Times.Once);  //check if GetTypesAsync has been called in service method (above line) only one time or not, if did so test will pass otherwise test will be failed
    }

    [Fact]
    public void ShouldCallGetProductTypeByIdAsyncWhenCallingGetTypeByIdMethod()
    {
        _service.GetTypeById(1);
        //check if GetProductTypeByIdAsync has been called in service method (above line) only one time or not, if did so test will pass otherwise test will be failed
        _repositoryMock.Verify(r => r.GetProductTypeByIdAsync(It.IsAny<int>()),Times.Once);
    }
}