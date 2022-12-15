using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class BrandsController :BaseApiController
{
    private readonly IBrandService _service;
    private readonly IMapper _mapper;


    public BrandsController(IBrandService service , IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
    {
        return  Ok( await _service.GetAllBrands());
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
    
    [HttpPost]
    public async Task<ActionResult<ProductBrand>> CreateBrand(ProductBrandToCreateDTO dto)
    {
        try
        {
            var brand = _mapper.Map<ProductBrandToCreateDTO, ProductBrand>(dto);
            var result =  _service.CreateBrand(dto);
            return Ok(brand);
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
    public ActionResult<ProductBrand> DeleteBrand(int id)
    {
        try
        {
            return Ok(_service.DeleteBrand(id));
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
}