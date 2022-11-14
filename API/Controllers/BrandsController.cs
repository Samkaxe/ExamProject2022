using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController :ControllerBase
{
    private readonly IBrandRepository _repository;

    public BrandsController(IBrandRepository repository)
    {
       _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        return  Ok( await _repository.GetBrandsAsync());
    }
}