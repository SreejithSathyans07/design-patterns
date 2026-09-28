using System;

namespace BetterApproach.Strategies;

public class SmsNotificationStrategy : INotificationStrategy
{
    public void Send(Order order, string message)
    {
        Console.WriteLine($"[SMS to {order.Phone}] {message}");
    }
}
