using BadApproach;

var orderService = new OrderService();
var reportService = new OrderReportService();

orderService.PlaceOrder("Jane Doe", 249.99m);
orderService.PlaceOrder("Jane Doe", 59.00m);
var toCancel = orderService.PlaceOrder("Jane Doe", 120.00m);
orderService.PlaceOrder("John Smith", 75.50m);

orderService.CancelOrder(toCancel.Id);

reportService.PrintActiveOrdersFor("Jane Doe");
reportService.PrintTotalRevenue();
