using MessageClient.Factory;
using Messages;

namespace ShippingService;

public static class ShippingServiceFactory
{
    public static ShippingService CreateShippingService(string queueName)
    {
        var easyNetQFactory = new EasyNetQFactory();
        var messageClient = easyNetQFactory.CreatePubSubMessageClient<OrderResponseMessage>(queueName);
        
        return new ShippingService(messageClient);
    }
}