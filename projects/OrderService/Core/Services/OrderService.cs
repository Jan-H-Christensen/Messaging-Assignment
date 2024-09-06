using OrderService.Core.Entities;
using Repository;

namespace OrderService.Core.Services;

// TODO: Modify this service as needed
public class OrderService
{
    private readonly IRepository<Order> _repository;
    public OrderService(IRepository<Order> repository)
    {
        _repository = repository;
    }
    
    public Order GetOrder(int id)
    {
        return _repository.GetById(id);
    }
    
    public void CreateOrder(Order order)
    {
        _repository.Add(order);
    }

    public void CompleteOrder(Order order)
    {
        _repository.Update(order);
    }
}