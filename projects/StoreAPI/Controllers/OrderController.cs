using MessageClient;
using Messages;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

internal static class MessageWaiter
{
    public static async Task<T?>? WaitForMessage<T>(MessageClient<T> messageClient, string clientId, int timeout = 5000)
    {
        var tcs = new TaskCompletionSource<T?>();
        var cancellationTokenSource = new CancellationTokenSource(timeout);
        cancellationTokenSource.Token.Register(() => tcs.TrySetResult(default));
        
        using (
            var connection = messageClient.ListenUsingTopic<T>(message =>
            {
                tcs.TrySetResult(message);
            }, "customer" + clientId, clientId)
        )
        {
        }

        return await tcs.Task;
    }
}

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly MessageClient<OrderResponseMessage> _orderResponseMessageClient;
    private readonly MessageClient<OrderRequestMessage> _orderRequestMessageClient;
    
    public OrderController(MessageClient<OrderResponseMessage> orderResponseMessageClient, MessageClient<OrderRequestMessage> orderRequestMessageClient)
    {
        _orderResponseMessageClient = orderResponseMessageClient;
        _orderRequestMessageClient = orderRequestMessageClient;
    }
    
    [HttpPost]
    public async Task<ActionResult<string>> PostOrder(Order order)
    {
        // Sends new order request message using 'newOrder' topic
        _orderRequestMessageClient.SendUsingTopic(new OrderRequestMessage
        {
            CustomerId = order.CustomerId,
            Status = "Order received."
        }, "newOrder");
        
        // Waits for 'OrderResponseMessage' using 'customerId' topic
        var response = await MessageWaiter.WaitForMessage(_orderResponseMessageClient, order.CustomerId)!;
        
        // Returns the status of the order
        return response != null ? response.Status : "Order timed out.";
    }
}