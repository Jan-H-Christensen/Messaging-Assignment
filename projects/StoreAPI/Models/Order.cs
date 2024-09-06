namespace StoreAPI.Models;

public class Order
{
    public string CustomerId { get; set; }
    public string Status { get; set; }
    public int propId { get; set; }
    public int PropAmount { get; set; }
}