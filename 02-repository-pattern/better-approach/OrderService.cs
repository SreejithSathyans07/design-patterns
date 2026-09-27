namespace BetterApproach;

public class OrderService
{
    // Storage is no longer OrderService's concern at all - it depends only
    // on the IOrderRepository abstraction, injected via the constructor.
    private readonly IOrderRepository _orderRepo;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepo = orderRepository;
    }

    public Order PlaceOrder(string customerName, decimal total)
    {
        var order = new Order
        {
            CustomerName = customerName,
            Total = total,
            Status = OrderStatus.Pending
        };

        _orderRepo.Add(order);
        return order;
    }

    public void CancelOrder(int orderId)
    {
        var found = _orderRepo.GetOrderById(orderId);
        if (found is null)
        {
            throw new InvalidOperationException($"Order #{orderId} not found.");
        }

        found.Status = OrderStatus.Cancelled;
        _orderRepo.Update(found);
    }

    public List<Order> GetOrdersByCustomer(string customerName)
    {
        return _orderRepo.GetOrdersByCustomer(customerName);
    }
}
