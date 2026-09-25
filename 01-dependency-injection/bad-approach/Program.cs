using BadApproach;

var order = new Order
{
    Id = 1001,
    CustomerName = "Jane Doe",
    CustomerEmail = "jane.doe@example.com",
    Total = 249.99m
};

var orderService = new OrderService();
orderService.ProcessOrder(order);
