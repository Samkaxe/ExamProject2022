using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TypesController :ControllerBase
{
    private readonly ITypeRepository _repository;

    public TypesController(ITypeRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
    {
        return  Ok(await _repository.GetTypesAsync());
    }
}