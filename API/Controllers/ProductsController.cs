using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class ProductsController :BaseApiController
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;


    public ProductsController(IProductService service , IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var products = await _service.GetAllProducts();
        var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        try
        {
            var product = await _service.GetProductById(id);
            return  Ok(_mapper.Map<Product, ProductToReturnDto>(product));
            
        }
        catch (KeyNotFoundException e) { return NotFound("No product found at ID " + id); }
        catch (Exception e) { return StatusCode(500, e.ToString()); }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateNewProduct(ProductToCreateDTO dto)
    {
        try
        {
            var product = _mapper.Map<ProductToCreateDTO, Product>(dto);
            var result =  _service.CreateNewProduct(dto);
            return Ok(result);
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
    
    [HttpPut]
    [Route("{id}")] 
    public ActionResult<Product> UpdateProduct([FromRoute]int id, [FromBody]Product product)
    {
        try
        {
            return Ok(_service.UpdateProduct(id, product));
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpDelete]
    [Route("{id}")]
    public ActionResult<Product> DeleteProduct(int id)
    {
        try
        {
            return Ok(_service.DeleteProduct(id));
        } catch (KeyNotFoundException e) 
        {
            return NotFound("No product found at ID " + id);
        } catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }

}