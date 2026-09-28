namespace BetterApproach;

public class NotificationService
{
    public void NotifyOrderPlaced(Order order)
    {
        var message = $"Hi {order.CustomerName}, your order #{order.Id} for ${order.Total} has been placed!";

        // Which channel to use is decided right here, with an if/else chain.
        if (order.IsVip)
        {
            Console.WriteLine($"[SMS to {order.Phone}] {message}");
        }
        else if (order.PrefersPushNotifications)
        {
            Console.WriteLine($"[PUSH to {order.CustomerName}'s device] {message}");
        }
        else
        {
            Console.WriteLine($"[EMAIL to {order.Email}] {message}");
        }
    }
}
