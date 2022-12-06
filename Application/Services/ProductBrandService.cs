using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace Application.Services;

public class ProductBrandService : IBrandService
{
    private readonly IBrandRepository _repo;
    private readonly IValidator<ProductBrandToCreateDTO> _postValidator;
    private readonly IMapper _mapper;

    public ProductBrandService(IBrandRepository repo , IValidator<ProductBrandToCreateDTO> postValidator , IMapper mapper)
    {
        _repo = repo;
        _postValidator = postValidator;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductBrand>> GetAllBrands()
    {
        return await _repo.GetBrandsAsync();
    }

    public async Task<ProductBrand> GetProductBrandById(int id)
    {
        return await _repo.GetProductBrandByIdAsync(id);
    }

    public ProductBrand CreateBrand(ProductBrandToCreateDTO dto)
    {
        try
        {
            var validation = _postValidator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException("line 32 method create new product in product service ");

            return _repo.CreateBrand(_mapper.Map<ProductBrand>(dto));
        }
        catch (Exception e)
        {
            Console.WriteLine("line 46 ProductBrandRepo");
        }

        throw new Exception();
    }

    public ProductBrand DeleteBrand(int id)
    {
        return _repo.DeleteBrand(id);
    }
}