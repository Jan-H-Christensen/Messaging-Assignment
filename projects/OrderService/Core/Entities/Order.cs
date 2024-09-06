using Microsoft.EntityFrameworkCore;

namespace OrderService.Core.Entities;

[Keyless]
public class Order
{
    public int OrderId { get; set; }
}