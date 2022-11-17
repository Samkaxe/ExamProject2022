using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class TypesController :BaseApiController
{
    private readonly ITypeService _service;


    public TypesController(ITypeService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
    {
        return  Ok(await _service.GetAllTypes());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductBrand(int id)
    {
        try
        {
            return await _service.GetTypeById(id);
        }
        catch (KeyNotFoundException e) { return NotFound("No Type found at ID " + id); }
        catch (Exception e) { return StatusCode(500, e.ToString()); }
    }
}