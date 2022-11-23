using Application.Services;
using Core.Interfaces;
using Moq;

namespace Application.Test;

public class ProductBrandServiceTest
{
    private Mock<IBrandRepository> _repositoryMock;
    private ProductBrandService _service;

    public ProductBrandServiceTest()
    {
        _repositoryMock = new Mock<IBrandRepository>();
        _service = new ProductBrandService(_repositoryMock.Object);
    }

    [Fact]
    public void ShouldCallGetBrandsAsyncFromRepositoryToGetAllTypes()
    {
        _service.GetAllTypes();
        _repositoryMock.Verify(repository => repository.GetBrandsAsync(), Times.Once);
    }

    [Fact]
    public void ShouldCallGetProductBrandByIdAsyncToGetBrandById()
    {
        _service.GetProductBrandById(1);
        _repositoryMock.Verify(repository => repository.GetProductBrandByIdAsync(It.IsAny<int>()), Times.Once());
    }
}