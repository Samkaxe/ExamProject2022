using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Data;

public class BasketRepository : IBasketRepository
{
    // package Stack exchange from rids  
    //cannot have its value changed by a subsequent assignment and cannot be unset
    private readonly IDatabase _database;
   
    //this is the object that hides away the details of multiple servers
    // it is designed to be shared and reused between callers
    //It is fully thread-safe and ready for this usage
    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket> GetBasket(string id)
    {
        var data = await _database.StringGetAsync(id);// basket will be stored as strings in the ridis 
        // the idea here is we serialize and deserialize objects come in and out from the basket  
        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
    }
    // we could use this method to update or create basket 
    public async Task<CustomerBasket> UpdateBasket(CustomerBasket basket)
    {
        // StringSetAsync
        //Set key to hold the string value. If key already holds a value,
        //it is overwritten, regardless of its type.
        //key – The key of the string.
        //value – The value to set.
        //expiry – The expiry to set
        var created = await _database.StringSetAsync(
            basket.Id,
            JsonSerializer.Serialize(basket),
            TimeSpan.FromDays(30)
            );

        if (!created) return null;

        return await GetBasket(basket.Id);
    }

    public async Task<bool> DeleteBasket(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId); // delete the key 
    }
    // its just place were customer store their basket in the memory 
    // so if they come back to it 
    // what we store on the client side is the basket id and we use that to get the right basket
    // if they left the basket for month it will be destroyed
    // redis is cool it take snapshot of the memory every minutes or so save it on disc 
    // need to see the difference  between an order and basket 
}