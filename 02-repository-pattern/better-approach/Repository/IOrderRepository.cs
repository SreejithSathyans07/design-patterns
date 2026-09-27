using System;

namespace BetterApproach;

public interface IOrderRepository
{
    void Add(Order order);
    void Update(Order order);
    List<Order> GetOrders();
    Order? GetOrderById(int orderId);
    List<Order> GetOrdersByCustomer(string customerName);
}
