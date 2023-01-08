namespace Core.Entities;

public class CustomerBasket
{
    public CustomerBasket()
    {
    }
    // just in case we need to create constructor without the id 

    public CustomerBasket(string id)
    {
        Id = id;
    }

    public string Id { get; set; } // i set it string for now because am using uuid generator 
    //since we create instance of the list we dont need to add it to the constructor 
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}