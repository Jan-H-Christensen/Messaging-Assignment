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
        _messageClient.ConnectAndListen(HandleOrderShippingCalculation);
    }
    
    private void HandleOrderShippingCalculation(OrderRequestMessage orderResponse)
    {
        Console.WriteLine("HandleOrderShippingCalculation OrderShippingCalculation");
        var shippingFee = 50;
        var calculate_price = orderResponse.Price*orderResponse.PropAmount + shippingFee;
        orderResponse.Price = calculate_price;
        _messageClient.SendUsingTopic(orderResponse, "orderCompletion");
    }
}