using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;

namespace Application.Services;

public class ProductTypeService : ITypeService
{
    private readonly ITypeRepository _repository;
    private readonly IValidator<ProductTypeToCreateDTO> _postValidator;
    private readonly IMapper _mapper;

    public ProductTypeService(ITypeRepository repository , IValidator<ProductTypeToCreateDTO> postValidator , IMapper mapper)
    {
        _repository = repository;
        _postValidator = postValidator;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductType>> GetAllTypes()
    {
        return await _repository.GetTypesAsync();
    }

    public async Task<ProductType> GetTypeById(int id)
    {
        return await _repository.GetProductTypeByIdAsync(id);
    }

    public ProductType CreateType(ProductTypeToCreateDTO dto)
    {
        try
        {
            var validation = _postValidator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException("line 32 method create new product in product service ");

            return _repository.CreateType(_mapper.Map<ProductType>(dto));
        }
        catch (Exception e)
        {
            Console.WriteLine("line 46 ProductType");
        }
        throw new Exception();
    }

    public ProductType DeleteType(int id)
    {
        return _repository.DeleteType(id);
    }
}