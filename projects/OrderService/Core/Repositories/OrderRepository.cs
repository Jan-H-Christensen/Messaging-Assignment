using OrderService.Core.Entities;
using Repository;
using OrderService.Core.Helpers;

namespace OrderService.Core.Repositories;

public class OrderRepository : EntityFrameworkRepository<Order>
{
    public OrderRepository(DataContext context) : base(context) { }
}