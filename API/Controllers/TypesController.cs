using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class TypesController :BaseApiController
{
   
    private readonly ITypeService _service;
    private readonly IMapper _mapper;


    public TypesController(ITypeService service , IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
    {
        return  Ok(await _service.GetAllTypes());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductType(int id)
    {
        try
        {
            return await _service.GetTypeById(id);
        }
        catch (KeyNotFoundException e) { return NotFound("No Type found at ID " + id); }
        catch (Exception e) { return StatusCode(500, e.ToString()); }
    }
    
    [HttpPost]
    public async Task<ActionResult<ProductType>> CreateType(ProductTypeToCreateDTO dto)
    {
        try
        {
            var type = _mapper.Map<ProductTypeToCreateDTO, ProductType>(dto);
            var result =  _service.CreateType(dto);
            return Ok(type);
        }
        catch (ValidationException v)
        {
            return BadRequest(v.Message);
        }
        catch (System.Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<ProductType> DeleteType(int id)
    {
        try
        {
            return Ok(_service.DeleteType(id));
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
}