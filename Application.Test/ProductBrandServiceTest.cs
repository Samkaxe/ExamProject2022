using Application.Services;
using Core.Interfaces;
using Moq;

namespace Application.Test;

public class ProductBrandServiceTest
{
    private Mock<IBrandRepository> _repositoryMock;  //to mock IBrandRepository instead of implementing 
    private ProductBrandService _service;

    public ProductBrandServiceTest()
    {
        _repositoryMock = new Mock<IBrandRepository>();  //create mock
        _service = new ProductBrandService(_repositoryMock.Object);
    }

    [Fact]
    public void ShouldCallGetBrandsAsyncFromRepositoryToGetAllTypes()
    {
        _service.GetAllTypes();
        _repositoryMock.Verify(repository => repository.GetBrandsAsync(), Times.Once);   //check if GetBrandsAsync has been called in service method (above line) only one time or not, if did so test will pass otherwise test will be failed
    }

    [Fact]
    public void ShouldCallGetProductBrandByIdAsyncToGetBrandById()
    {
        _service.GetProductBrandById(1);
        //check if GetProductBrandByIdAsync has been called in service method (above line) only one time or not, if did so test will pass otherwise test will be failed
        _repositoryMock.Verify(repository => repository.GetProductBrandByIdAsync(It.IsAny<int>()), Times.Once());
    }
}