namespace ShippingService
{
    class Program
    {
        static void Main(string[] args)
        {
            var shippingService = ShippingServiceFactory.CreateShippingService("");
            shippingService.Start();
      
            Console.WriteLine("Shipping Service started. Press any key to exit.");
            
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