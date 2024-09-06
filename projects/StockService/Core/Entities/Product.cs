namespace StockService.Core.Entities;

public class Product
{
    public Product(int id, string name, int stock, decimal price)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
    }
    public int Id { get; }
    public string Name { get; }
    public int Stock { get; set; }
    public decimal Price { get; }
}