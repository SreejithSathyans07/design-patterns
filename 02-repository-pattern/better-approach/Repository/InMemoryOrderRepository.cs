using System;

namespace BetterApproach;

public class InMemoryOrderRepository : IOrderRepository
{
    private List<Order> Orders = new();
    private int _nextId = 1;

    public void Add(Order order)
    {
        order.Id = _nextId++;
        Orders.Add(order);
    }

    public Order? GetOrderById(int orderId)
    {
        Order? order = Orders.Find(o => o.Id == orderId);
        return order;
    }

    public List<Order> GetOrders()
    {
        return [.. Orders];
    }

    public List<Order> GetOrdersByCustomer(string customerName)
    {
        return Orders.FindAll(order => order.CustomerName == customerName);
    }

    public void Update(Order order)
    {
        Order? existingOrder = Orders.Find(o => o.Id == order.Id);
        if(existingOrder == null){
            throw new KeyNotFoundException("Order doesn't exists");
        }
        existingOrder.CustomerName = order.CustomerName;
        existingOrder.Status = order.Status;
        existingOrder.Total = order.Total;
    }
}
