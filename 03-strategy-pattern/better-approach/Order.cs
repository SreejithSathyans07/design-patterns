namespace BetterApproach;

public class Order
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public bool IsVip { get; set; }
    public bool PrefersPushNotifications { get; set; }
}
