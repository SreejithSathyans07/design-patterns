namespace BetterApproach;

public class OrderService
{
    private readonly NotificationService _notificationService = new();
    private int _nextId = 1;

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
