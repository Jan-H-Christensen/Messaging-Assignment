using Repository;
using StockService.Core.Entities;
using StockService.Core.Helpers;

namespace StockService.Core.Repositories;

public class ProductRepository : EntityFrameworkRepository<Product> {
  public ProductRepository(DataContext context) : base(context) { }
}