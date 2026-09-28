using System;

namespace BetterApproach.Strategies;

public class EmailNotificationStrategy : INotificationStrategy
{
    public void Send(Order order, string message)
    {
            Console.WriteLine($"[EMAIL to {order.Email}] {message}");
    }
}
