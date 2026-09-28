using BetterApproach;
using BetterApproach.Strategies;

NotificationStrategyFactory notificationStrategyFactory = new();
NotificationService notificationService = new(notificationStrategyFactory);
var reminderService = new ReminderService(notificationStrategyFactory);
var orderService = new OrderService(notificationService);

var vipOrder = orderService.PlaceOrder("Jane Doe", "jane@example.com", "555-0100", 249.99m, isVip: true, prefersPush: false);
var pushOrder = orderService.PlaceOrder("John Smith", "john@example.com", "555-0200", 59.00m, isVip: false, prefersPush: true);
var regularOrder = orderService.PlaceOrder("Alice Lee", "alice@example.com", "555-0300", 75.50m, isVip: false, prefersPush: false);

Console.WriteLine();
Console.WriteLine("--- Sending payment reminders ---");
reminderService.SendPaymentReminder(vipOrder);
reminderService.SendPaymentReminder(pushOrder);
reminderService.SendPaymentReminder(regularOrder);
