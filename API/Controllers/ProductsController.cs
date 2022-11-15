using Application.DTOs;
using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;


    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _service.GetAllProducts();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        try
        {
            return await _service.GetProductById(id);
        }
        catch (KeyNotFoundException e) { return NotFound("No product found at ID " + id); }
        catch (Exception e) { return StatusCode(500, e.ToString()); }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateNewProduct(ProductToCreateDTO dto)
    {
        try
        {
            var result = _service.CreateNewProduct(dto);
            return Created("", result);
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