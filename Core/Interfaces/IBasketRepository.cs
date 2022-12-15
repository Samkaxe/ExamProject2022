using Core.Entities;

namespace Core.Interfaces;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasket(string id);

    Task<CustomerBasket> UpdateBasket(CustomerBasket basket);

    Task<bool> DeleteBasket(string basketId);
}