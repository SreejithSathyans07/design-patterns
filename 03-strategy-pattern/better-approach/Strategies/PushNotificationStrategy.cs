using System;

namespace BetterApproach.Strategies;

public class PushNotificationStrategy : INotificationStrategy
{
    public void Send(Order order, string message)
    {
            Console.WriteLine($"[PUSH to {order.CustomerName}'s device] {message}");
    }
}
