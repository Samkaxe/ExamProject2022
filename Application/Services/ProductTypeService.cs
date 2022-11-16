using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class ProductTypeService : ITypeService
{
    private readonly ITypeRepository _repository;

    public ProductTypeService(ITypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductType>> GetAllTypes()
    {
        return await _repository.GetTypesAsync();
    }

    public async Task<ProductType> GetTypeById(int id)
    {
        return await _repository.GetProductTypeByIdAsync(id);
    }
}