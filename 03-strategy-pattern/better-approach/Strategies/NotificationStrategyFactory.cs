using System;

namespace BetterApproach.Strategies;

public class NotificationStrategyFactory
{
    public INotificationStrategy GetStrategy(Order order)
    {
        if (order.IsVip)
        {
            return new SmsNotificationStrategy();
        }
        else if (order.PrefersPushNotifications)
        {
            return new PushNotificationStrategy();
        }
        else
        {
            return new EmailNotificationStrategy();
        }
    }
}
