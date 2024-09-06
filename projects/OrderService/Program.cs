namespace OrderService
{
  class Program
  {
    static void Main(string[] args)
    {
      var orderService = OrderServiceFactory.CreateOrderService();
      orderService.Start();      
      
      Console.WriteLine("Order Service started. Press any key to exit.");
            
      var running = true;
      AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
      {
          Console.WriteLine("Exiting...");
          running = false;
      };
            
      while (running)
      {
      }
    }
  }
}