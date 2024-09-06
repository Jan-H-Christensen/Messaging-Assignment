using MessageClient;
using Messages;
using OrderService.Core.Mappers;

namespace OrderService;

public class OrderService
{
  private readonly MessageClient<OrderRequestMessage> _newOrderClient;
  private readonly MessageClient<OrderRequestMessage> _orderCompletionClient;
  private readonly MessageClient<OrderResponseMessage> _apiOrderCompletionClient;
  private readonly Core.Services.OrderService _orderService;
  private readonly OrderResponseMapper _orderResponseMapper;
  public OrderService(MessageClient<OrderRequestMessage> newOrderClient, MessageClient<OrderRequestMessage> orderCompletionClient, MessageClient<OrderResponseMessage> apiOrderCompletionClient, Core.Services.OrderService orderService, OrderResponseMapper orderResponseMapper)
  {
    _newOrderClient = newOrderClient;
    _orderCompletionClient = orderCompletionClient;
    _orderService = orderService;
    _orderResponseMapper = orderResponseMapper;
    _apiOrderCompletionClient = apiOrderCompletionClient;
  }

  public void Start()
  {
    // Start listening for new orders
    _newOrderClient.ConnectAndListen(HandleNewOrder);

    // Connect to the order completion topic
    _orderCompletionClient.ConnectAndListen(HandleOrderCompletion);

    _apiOrderCompletionClient.Connect();
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    if (order.propId <= 0)
    {
      var response = new OrderResponseMessage
      {
        CustomerId = order.CustomerId,
        Status = "No product was given",
        propId = order.propId,
        PropAmount = order.PropAmount,
        Price = order.Price
      };

      Console.WriteLine("no Product was given");

      apiMessage(response);
    }

    Console.WriteLine("HandleNewOrder Order");
    _newOrderClient.SendUsingTopic(order, "NewOrderStock");
  }

  private void HandleOrderCompletion(OrderRequestMessage order)
  {
    // Create new OrderResponseMessage
    Console.WriteLine($"Completed order from customer {order.CustomerId}");
    var response = new OrderResponseMessage
    {
      CustomerId = order.CustomerId,
      Status = "Order completed",
      propId = order.propId,
      PropAmount = order.PropAmount,
      Price = order.Price
    };

    apiMessage(response);
  }

  private void apiMessage(OrderResponseMessage order)
  {
    Console.WriteLine($"Sending order status to customer {order.CustomerId}");
    _apiOrderCompletionClient.SendUsingTopic<OrderResponseMessage>(order,
    order.CustomerId);
  }
}