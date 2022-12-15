﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string Id)
    {
        var basket = await _repository.GetBasket(Id);
        return Ok(basket ?? new CustomerBasket(Id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updateBasket = await _repository.UpdateBasket(basket);
        return Ok(updateBasket);
    }
    
    [HttpDelete]
    public async Task DeleteBasket(string id)
    {
        await _repository.DeleteBasket(id);
    }
}