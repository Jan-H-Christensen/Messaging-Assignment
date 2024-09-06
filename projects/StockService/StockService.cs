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
    // TODO: Start listening for new orders
  }

  private void HandleNewOrder(OrderRequestMessage order)
  {
    // TODO: Handle the new orders
    /*
     * TODO: Handle new orders
     * - Check the stock of the products in the order
     * - Create a new order response with the stock status of the products
     * - Send the order response so the shipping service can calculate the shipping cost
     */
  }
}