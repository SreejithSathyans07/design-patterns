using System;

namespace BetterApproach.Tests.Fakes;

public class FakeOrderRepository : IOrderRepository
{
    private List<Order> fakeOrderList = new List<Order>();
    private int _nextId = 1;

    public List<Order> GetOrderList()
    {
        return fakeOrderList;
    }
    public void Add(Order order)
    {
        order.Id = _nextId++;
        fakeOrderList.Add(order);
    }

    public Order? GetOrderById(int orderId)
    {
        return fakeOrderList.Find(o => o.Id == orderId);
    }

    public List<Order> GetOrders()
    {
        return new List<Order>(fakeOrderList);
    }

    public List<Order> GetOrdersByCustomer(string customerName)
    {
        return fakeOrderList.FindAll(order => order.CustomerName == customerName);
    }

    public void Update(Order order)
    {
        // No-op: Order is a reference type, and OrderService always mutates
        // the exact same object it got from GetOrderById, so the change is
        // already reflected in fakeOrderList. This method just needs to
        // exist and not throw, so OrderService's call to it succeeds.
    }
}
