namespace BetterApproach;

public class OrderService
{
    // "The database." A raw in-memory list, manipulated directly wherever
    // it's needed. It's public static so OrderReportService can get at it
    // too - there's no other way for a second class to see these orders.
    public static List<Order> Orders = new();

    private int _nextId = 1;

    public Order PlaceOrder(string customerName, decimal total)
    {
        var order = new Order
        {
            Id = _nextId++,
            CustomerName = customerName,
            Total = total,
            Status = OrderStatus.Pending
        };

        Orders.Add(order);
        return order;
    }

    public void CancelOrder(int orderId)
    {
        // Hand-rolled lookup logic, written directly here.
        Order? found = null;
        foreach (var order in Orders)
        {
            if (order.Id == orderId)
            {
                found = order;
                break;
            }
        }

        if (found is null)
        {
            throw new InvalidOperationException($"Order #{orderId} not found.");
        }

        found.Status = OrderStatus.Cancelled;
    }

    public List<Order> GetOrdersByCustomer(string customerName)
    {
        // Another hand-rolled loop, slightly different shape from the one
        // in CancelOrder, even though the "intent" is similar: search Orders.
        var result = new List<Order>();
        foreach (var order in Orders)
        {
            if (order.CustomerName == customerName)
            {
                result.Add(order);
            }
        }

        return result;
    }
}
