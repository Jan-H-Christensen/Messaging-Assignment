namespace StockService
{
  class Program
  {
    static void Main(string[] args)
    {
      var stockService = StockServiceFactory.CreateStockService("");
      stockService.PopulateDb();
      stockService.Start();
      
      Console.WriteLine("Stock Service started. Press any key to exit.");
            
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