using Messages;
using OrderService.Core.Repositories;
using OrderService.Core.Helpers;

namespace OrderService;
using MessageClient.Factory;

public static class OrderServiceFactory
{
  public static OrderService CreateOrderService()
  {
    var easyNetQFactory = new EasyNetQFactory();
    var newOrderClient = easyNetQFactory.CreateTopicMessageClient<OrderRequestMessage>("OrderService", "newOrder");
    var orderCompletionClient = easyNetQFactory.CreatePubSubMessageClient<OrderResponseMessage>("");
    
    var dataContext = new DataContext();
    var orderRepository = new OrderRepository(dataContext);
    var orderService = new Core.Services.OrderService(orderRepository);
    var orderResponseMapper = new Core.Mappers.OrderResponseMapper();
    
    return new OrderService(
      newOrderClient,
      orderCompletionClient,
      orderService,
      orderResponseMapper
    );
  }
}