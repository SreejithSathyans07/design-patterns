namespace BetterApproach;

public class ReminderService
{
    public void SendPaymentReminder(Order order)
    {
        var message = $"Hi {order.CustomerName}, a friendly reminder that ${order.Total} is due for order #{order.Id}.";

        // Written independently of NotificationService's channel-selection
        // logic - and it shows: this one never learned about push
        // notifications, so PrefersPushNotifications customers silently
        // fall through to email instead.
        if (order.IsVip)
        {
            Console.WriteLine($"[SMS to {order.Phone}] {message}");
        }
        else
        {
            Console.WriteLine($"[EMAIL to {order.Email}] {message}");
        }
    }
}
