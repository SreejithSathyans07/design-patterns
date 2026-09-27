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
    //
    // Each "Print..." method is split into a computation (returns a real
    // value, so it's testable without capturing Console output) and a thin
    // wrapper that just prints the result - the same untestable-Console
    // smell we called out in bad-approach back in pattern 01.
    public List<Order> GetActiveOrdersFor(string customerName)
    {
        return _orderRepo.GetOrdersByCustomer(customerName)
            .Where(o => o.Status != OrderStatus.Cancelled)
            .ToList();
    }

    public void PrintActiveOrdersFor(string customerName)
    {
        var activeOrders = GetActiveOrdersFor(customerName);
        Console.WriteLine($"Active orders for {customerName}:");
        foreach (var order in activeOrders)
        {
            Console.WriteLine($"  Order #{order.Id} - ${order.Total} - {order.Status}");
        }
    }

    public decimal CalculateTotalRevenue()
    {
        return _orderRepo.GetOrders()
            .Where(o => o.Status != OrderStatus.Cancelled)
            .Sum(o => o.Total);
    }

    public void PrintTotalRevenue()
    {
        Console.WriteLine($"Total revenue (excluding cancelled orders): ${CalculateTotalRevenue()}");
    }
}
