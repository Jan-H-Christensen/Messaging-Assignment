using MessageClient;
using Messages;

namespace ShippingService;

public class ShippingService
{
    private readonly MessageClient<OrderResponseMessage> _messageClient;
    
    public ShippingService(MessageClient<OrderResponseMessage> messageClient)
    {
        _messageClient = messageClient;
    }
    
    public void Start()
    {
        // TODO: Start listening for orders that need to be shipped
    }
    
    private void HandleOrderShippingCalculation(OrderResponseMessage orderResponse)
    {
        /*
         * TODO: Handle the calculation of the shipping cost for the order
         * - Calculate the shipping cost
         * - Change the status of the order and apply the shipping cost
         * - Send the processed order to the order service for completion
         */
    }
}