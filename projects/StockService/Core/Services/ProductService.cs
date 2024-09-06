using Repository;
using StockService.Core.Entities;

namespace StockService.Core.Services;

public class ProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public void PopulateDb()
    {
        // Populate the database with some products
        _repository.Add(new Product
        {
            Id = 1,
            Name = "Product 1",
            Stock = 10,
            Price = 100
        });
        _repository.Add(new Product
        {
            Id = 2,
            Name = "Product 2",
            Stock = 20,
            Price = 200
        });
        _repository.Add(new Product
        {
            Id = 3,
            Name = "Product 3",
            Stock = 30,
            Price = 300
        });
    }

    public IEnumerable<Product> GetOrderProducts(int[] productIds)
    {
        var products = new List<Product>();
        foreach (var productId in productIds)
        {
            var product = _repository.GetById(productId);
            if (product != null)
            {
                products.Add(product);
            }
        }
        return products;
    }

    public IEnumerable<Product> GetProducts()
    {
        return _repository.GetAll();
    }
}