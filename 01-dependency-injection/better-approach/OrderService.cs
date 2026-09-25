namespace BetterApproach;

public class OrderService
{
    // OrderService reaches out and builds its own dependencies.
    // It decides *what* logger and *what* notifier to use, not just
    // *how* to use them.
    private readonly ConsoleLogger _logger = new();
    private readonly EmailNotifier _notifier = new();

    public void ProcessOrder(Order order)
    {
        _logger.Log($"Processing order #{order.Id} for {order.CustomerName}...");

        // Pretend to charge a credit card.
        _logger.Log($"Charging ${order.Total} to credit card...");
        _logger.Log("Payment successful.");

        _notifier.Send(
            order.CustomerEmail,
            $"Hi {order.CustomerName}, your order #{order.Id} for ${order.Total} has been placed!");

        _logger.Log($"Order #{order.Id} processed successfully.");
    }
}
