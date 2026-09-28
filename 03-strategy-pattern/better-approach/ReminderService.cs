using BetterApproach.Strategies;

namespace BetterApproach;

public class ReminderService
{
    private readonly NotificationStrategyFactory _notificationStrategyFactory;

    public ReminderService(NotificationStrategyFactory notificationStrategyFactory)
    {
        _notificationStrategyFactory = notificationStrategyFactory;
    }

    public void SendPaymentReminder(Order order)
    {
        var message = $"Hi {order.CustomerName}, a friendly reminder that ${order.Total} is due for order #{order.Id}.";

        var notificationStrategy = _notificationStrategyFactory.GetStrategy(order);
        notificationStrategy.Send(order, message);
    }
}
