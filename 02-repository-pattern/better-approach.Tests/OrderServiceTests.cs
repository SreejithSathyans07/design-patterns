using BetterApproach.Tests.Fakes;

namespace BetterApproach.Tests;

public class OrderServiceTests
{
    private readonly OrderService _orderService;
    FakeOrderRepository fakeOrderRepository = new FakeOrderRepository();

    public OrderServiceTests()
    {
        _orderService = new OrderService(fakeOrderRepository);
    }

    [Fact]
    public void Order_Service_Should_Place_Order()
    {
        _orderService.PlaceOrder("John", 50);
        var fakeOrderList = fakeOrderRepository.GetOrderList();

        Assert.Equal("John", fakeOrderList[0].CustomerName);
    }
    [Fact]
    public void CancelOrder_ShouldSetStatusToCancelled_WhenOrderExists()
    {
        var order = _orderService.PlaceOrder("James", 20);
        _orderService.CancelOrder(order.Id);
        Assert.Equal(OrderStatus.Cancelled, fakeOrderRepository.GetOrderById(order.Id)?.Status);
    }
    [Fact]
    public void CancelOrder_ShouldThrow_WhenOrderDoesNotExist()
    {
        Assert.Throws<InvalidOperationException>(() => _orderService.CancelOrder(999));
    }
    [Fact]
    public void GetOrdersByCustomer_ShouldReturnAllOrdersForThatCustomer()
    {
        string name = "John";
        var order1 = _orderService.PlaceOrder(name, 50);
        var order2 = _orderService.PlaceOrder(name, 20);
        var retrievedOrder = _orderService.GetOrdersByCustomer(order1.CustomerName);
        Assert.Equal(2, retrievedOrder.Count);

    }
}
