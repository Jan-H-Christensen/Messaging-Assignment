using Messages;
using StockService.Core.Services;

namespace StockService;

using MessageClient;

public class StockService
{
  private readonly MessageClient<OrderRequestMessage> _messageClient;
  private readonly ProductService _productService;

  public StockService(MessageClient<OrderRequestMessage> messageClient, ProductService productService)
  {
    _messageClient = messageClient;
    _productService = productService;
  }

  public void PopulateDb()
  {
    // Populate the database with some products
    _productService.PopulateDb();
  }

  public void Start()
  {
    // connect to the order request topic
    _messageClient.ConnectAndListen(HandleNewOrder);

    _messageClient.Connect();
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    var prodTest = _productService.GetProducts().FirstOrDefault(p => p.Id == order.propId);

    if (order.PropAmount >= 0 && prodTest != null)
    {
      if ((prodTest.Stock-order.PropAmount) >= 0)
      {
        prodTest.Stock -= order.PropAmount;
        Console.WriteLine("HandleNewOrder Stock");
        order.Status = "Order Stock Ready";
        order.Price = prodTest.Price;
        _messageClient.SendUsingTopic(order, "newShippingOrder");
      }
      else
      {
        var response = new OrderResponseMessage
        {
          CustomerId = order.CustomerId,
          Status = "Not enough stock",
          propId = order.propId,
          PropAmount = order.PropAmount,
          Price = order.Price
        };
        apiMessage(response);
      }
    }
    else
    {
      var response = new OrderResponseMessage{
        CustomerId = order.CustomerId, 
        Status = "No product was given/Found",
        propId = order.propId,
        PropAmount = order.PropAmount,
        Price = order.Price
      };
      apiMessage(response);
    }
  }

  private void apiMessage(OrderResponseMessage order)
  {
    Console.WriteLine($"Sending order status to customer {order.CustomerId}");
    _messageClient.SendUsingTopic<OrderResponseMessage>(order,
    order.CustomerId);
  }
}