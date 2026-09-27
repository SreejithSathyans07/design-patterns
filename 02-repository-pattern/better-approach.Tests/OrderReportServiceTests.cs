using BetterApproach.Tests.Fakes;

namespace BetterApproach.Tests;

public class OrderReportServiceTests
{
    private readonly OrderService _orderService;
    private readonly OrderReportService _reportService;
    private readonly FakeOrderRepository _fakeOrderRepository = new();

    public OrderReportServiceTests()
    {
        _orderService = new OrderService(_fakeOrderRepository);
        _reportService = new OrderReportService(_fakeOrderRepository);
    }

    [Fact]
    public void GetActiveOrdersFor_ShouldExcludeCancelledOrders()
    {
        var active = _orderService.PlaceOrder("Jane", 100m);
        var cancelled = _orderService.PlaceOrder("Jane", 50m);
        _orderService.CancelOrder(cancelled.Id);

        var result = _reportService.GetActiveOrdersFor("Jane");

        Assert.Single(result);
        Assert.Equal(active.Id, result[0].Id);
    }

    [Fact]
    public void GetActiveOrdersFor_ShouldOnlyReturnOrdersForThatCustomer()
    {
        _orderService.PlaceOrder("Jane", 100m);
        _orderService.PlaceOrder("John", 200m);

        var result = _reportService.GetActiveOrdersFor("Jane");

        Assert.Single(result);
        Assert.Equal("Jane", result[0].CustomerName);
    }

    [Fact]
    public void CalculateTotalRevenue_ShouldExcludeCancelledOrders()
    {
        _orderService.PlaceOrder("Jane", 100m);
        var cancelled = _orderService.PlaceOrder("Jane", 50m);
        _orderService.CancelOrder(cancelled.Id);

        var total = _reportService.CalculateTotalRevenue();

        Assert.Equal(100m, total);
    }

    [Fact]
    public void CalculateTotalRevenue_ShouldSumAcrossAllCustomers()
    {
        _orderService.PlaceOrder("Jane", 100m);
        _orderService.PlaceOrder("John", 50m);

        var total = _reportService.CalculateTotalRevenue();

        Assert.Equal(150m, total);
    }
}
