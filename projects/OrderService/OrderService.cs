using MessageClient;
using Messages;
using OrderService.Core.Mappers;

namespace OrderService;

public class OrderService
{
  private readonly MessageClient<OrderRequestMessage> _newOrderClient;
  private readonly MessageClient<OrderResponseMessage> _orderCompletionClient;
  private readonly Core.Services.OrderService _orderService;
  private readonly OrderResponseMapper _orderResponseMapper;
  public OrderService(MessageClient<OrderRequestMessage> newOrderClient, MessageClient<OrderResponseMessage> orderCompletionClient, Core.Services.OrderService orderService, OrderResponseMapper orderResponseMapper)
  {
    _newOrderClient = newOrderClient;
    _orderCompletionClient = orderCompletionClient;
    _orderService = orderService;
    _orderResponseMapper = orderResponseMapper;
  }

  public void Start()
  {
    // Start listening for new orders
    _newOrderClient.ConnectAndListen(HandleNewOrder);
    
    // Connect to the order completion topic
    _orderCompletionClient.Connect();
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    /*
     * TODO: Handle new orders
     * - Check if the order is valid
     * - Create the order in the database (optional)
     * - Send the order to the stock service
     */
    
    // Create new OrderResponseMessage
    Console.WriteLine($"Received new order from customer {order.CustomerId}");
    var orderResponse = new OrderResponseMessage
    {
        CustomerId = order.CustomerId,
        Status = "Order completed"
    };
    
    // Send the order completion to the customer using the customer ID as the topic
    Console.WriteLine($"Sending order completion to customer {orderResponse.CustomerId}");
    _orderCompletionClient.SendUsingTopic<OrderResponseMessage>(orderResponse,
        orderResponse.CustomerId);
  }
  
  private void HandleOrderCompletion(OrderResponseMessage order)
  {
      /*
       * TODO: Handle the order completion, e.g. change the order status
       * - Update the order status in the database
       * - Notify the customer
       */
  }
}