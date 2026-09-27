namespace BetterApproach;

public class OrderReportService
{
    private readonly IOrderRepository _orderRepo;
    public OrderReportService(IOrderRepository repository)
    {
        _orderRepo = repository;
    }
    // Gets orders through the same IOrderRepository OrderService uses - no
    // more reaching into OrderService's storage or duplicating lookup logic.
    public void PrintActiveOrdersFor(string customerName)
    {
        var activeOrders = _orderRepo.GetOrdersByCustomer(customerName).Where(o => o.Status != OrderStatus.Cancelled);
        Console.WriteLine($"Active orders for {customerName}:");
        foreach (var order in activeOrders)
        {
            Console.WriteLine($"  Order #{order.Id} - ${order.Total} - {order.Status}");
        }
    }

    public void PrintTotalRevenue()
    {
        decimal total = 0;
        List<Order> allOrders = _orderRepo.GetOrders().Where(o => o.Status != OrderStatus.Cancelled).ToList();
        foreach (var order in allOrders)
        {
            total += order.Total;
        }

        Console.WriteLine($"Total revenue (excluding cancelled orders): ${total}");
    }
}
