namespace Messages;

public class OrderRequestMessage
{
    public string CustomerId { get; set; }
    public string Status { get; set; }
    public int propId { get; set; }
    public int PropAmount { get; set; }
    public decimal Price { get; set; }
}