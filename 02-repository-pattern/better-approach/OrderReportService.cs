namespace BetterApproach;

public class OrderReportService
{
    // Needs orders too, so it reaches straight into OrderService's storage
    // and writes its own, independent lookup logic against it.
    public void PrintActiveOrdersFor(string customerName)
    {
        var activeOrders = new List<Order>();
        foreach (var order in OrderService.Orders)
        {
            if (order.CustomerName == customerName && order.Status != OrderStatus.Cancelled)
            {
                activeOrders.Add(order);
            }
        }

        Console.WriteLine($"Active orders for {customerName}:");
        foreach (var order in activeOrders)
        {
            Console.WriteLine($"  Order #{order.Id} - ${order.Total} - {order.Status}");
        }
    }

    public void PrintTotalRevenue()
    {
        decimal total = 0;
        foreach (var order in OrderService.Orders)
        {
            if (order.Status != OrderStatus.Cancelled)
            {
                total += order.Total;
            }
        }

        Console.WriteLine($"Total revenue (excluding cancelled orders): ${total}");
    }
}
