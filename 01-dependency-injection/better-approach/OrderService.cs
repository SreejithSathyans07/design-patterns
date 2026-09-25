namespace BetterApproach;

public class OrderService
{
    private readonly IAppLogger _logger;
    private readonly INotifier _notifier;

    public OrderService(IAppLogger logger, INotifier notifier)
    {   
        _logger = logger;
        _notifier = notifier;
    }

    public void ProcessOrder(Order order)
    {
        _logger.Log($"Processing order #{order.Id} for {order.CustomerName}...");

        // Pretend to charge a credit card.
        _logger.Log($"Charging ${order.Total} to credit card...");
        _logger.Log("Payment successful.");

        _notifier.Notify(
            order.CustomerEmail,
            $"Hi {order.CustomerName}, your order #{order.Id} for ${order.Total} has been placed!");

        _logger.Log($"Order #{order.Id} processed successfully.");
    }
}
