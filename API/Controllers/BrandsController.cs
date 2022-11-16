using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController :ControllerBase
{
    private readonly IBrandService _service;
    

    public BrandsController(IBrandService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        return  Ok( await _service.GetAllTypes());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
    {
        try
        {
            return await _service.GetProductBrandById(id);
        }
        catch (KeyNotFoundException e) { return NotFound("No Brand found at ID " + id); }
        catch (Exception e) { return StatusCode(500, e.ToString()); }
    }
}