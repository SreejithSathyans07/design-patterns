using BetterApproach.Tests.fakes;

namespace BetterApproach.Tests;

public class OrderServiceTests
{
    //Arrange
    FakeLogger fakeLogger = new();
    FakeNotifier fakeNotifier = new();
    Order order = new() { Id = 1, CustomerName = "Sreejith", CustomerEmail = "sreejithsathyans@gmail.com", Total = 5 };

    [Fact]
    public void OrderService_Process_Order_Should_Sent_Notification()
    {
        var orderService = new OrderService(fakeLogger, fakeNotifier);

        //Act
        orderService.ProcessOrder(order);

        //Assert
        Assert.Single(fakeNotifier.Notifications);
        var notification = fakeNotifier.Notifications[0];
        Assert.Equal("sreejithsathyans@gmail.com", notification.ToAddress);
        Assert.Contains("Sreejith", notification.Message);
    }
    [Fact]
    public void OrderService_Process_Order_Should_Log_Messages()
    {
        var orderService = new OrderService(fakeLogger, fakeNotifier);

        //Act
        orderService.ProcessOrder(order);

        //Assert
        // Asserting on meaningful content instead of an exact count - an
        // unrelated extra/removed Log(...) call elsewhere in ProcessOrder
        // shouldn't break this test.
        Assert.Contains(fakeLogger.logMessages, m => m.Contains("Payment successful"));
    }

}
