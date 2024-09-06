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
        });
        _repository.Add(new Product
        {
        });
        _repository.Add(new Product
        {
        });
    }
    
    public IEnumerable<Product> GetOrderProducts(int[] productIds)
    {
        // TODO: Implement this method
        return new List<Product>();
    }
    
    public IEnumerable<Product> GetProducts()
    {
        return _repository.GetAll();
    }
}