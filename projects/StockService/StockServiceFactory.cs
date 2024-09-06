using Messages;
using MessageClient.Factory;
using StockService.Core.Helpers;
using StockService.Core.Repositories;
using StockService.Core.Services;

namespace StockService;

public static class StockServiceFactory
{
  public static StockService CreateStockService(string queueName)
  {
    var easyNetQFactory = new EasyNetQFactory();
    var messageClient = easyNetQFactory.CreatePubSubMessageClient<OrderRequestMessage>(queueName);
    
    var productRepository = new ProductRepository(new DataContext());
    var productService = new ProductService(productRepository);
    
    return new StockService(
      messageClient,
      productService
    );
  }
}