using Application.DTOs;
using Application.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
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
        _repositoryMock = new Mock<IBrandRepository>();
        _service = new ProductBrandService(_repositoryMock.Object , _createValidatorMock.Object , _mapperMock.Object);
       
    }

    [Fact]
    public void ShouldCallGetBrandsAsyncFromRepositoryToGetAllTypes()
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
}