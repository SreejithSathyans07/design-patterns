namespace BetterApproach;

public class OrderService
{
    private readonly NotificationService _notificationService;
    private int _nextId = 1;

    public OrderService(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public Order PlaceOrder(string customerName, string email, string phone, decimal total, bool isVip, bool prefersPush)
    {
        var order = new Order
        {
            Id = _nextId++,
            CustomerName = customerName,
            Email = email,
            Phone = phone,
            Total = total,
            IsVip = isVip,
            PrefersPushNotifications = prefersPush
        };

        _notificationService.NotifyOrderPlaced(order);
        return order;
    }
}
