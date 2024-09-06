using MessageClient;
using Messages;

namespace ShippingService;

public class ShippingService
{
    private readonly MessageClient<OrderRequestMessage> _messageClient;
    
    public ShippingService(MessageClient<OrderRequestMessage> messageClient)
    {
        _messageClient = messageClient;
    }
    
    public void Start()
    {
        // TODO: Start listening for orders that need to be shipped
        // Start listening for new orders
        _messageClient.ConnectAndListen(HandleOrderShippingCalculation);
        
        // Connect to the order completion topic
        _messageClient.Connect();
    }
    
    private void HandleOrderShippingCalculation(OrderRequestMessage orderResponse)
    {
        /*
         * TODO: Handle the calculation of the shipping cost for the order
         * - Calculate the shipping cost
         * - Change the status of the order and apply the shipping cost
         * - Send the processed order to the order service for completion
         */

        Console.WriteLine("HandleOrderShippingCalculation OrderShippingCalculation");
        /*_messageClient.SendUsingTopic(new OrderRequestMessage
        {
            CustomerId = orderResponse.CustomerId,
            Status = "Order received."
        }, "OrderCompletion");*/

        var orderResponses = new OrderResponseMessage
        {
            CustomerId = orderResponse.CustomerId,
            Status = "Order completed"
        };
        Console.WriteLine($"Sending order completion to customer {orderResponse.CustomerId}");
        _messageClient.SendUsingTopic<OrderResponseMessage>(orderResponses,
        "");
    }
}