using BetterApproach.Strategies;

namespace BetterApproach;

public class NotificationService
{
    private readonly NotificationStrategyFactory _notificationStrategyFactory;

    public NotificationService(NotificationStrategyFactory notificationStrategyFactory)
    {
        _notificationStrategyFactory = notificationStrategyFactory;
    }
    public void NotifyOrderPlaced(Order order)
    {
        var message = $"Hi {order.CustomerName}, your order #{order.Id} for ${order.Total} has been placed!";

        var notificationStrategy = _notificationStrategyFactory.GetStrategy(order);
        notificationStrategy.Send(order, message);
    }
}
