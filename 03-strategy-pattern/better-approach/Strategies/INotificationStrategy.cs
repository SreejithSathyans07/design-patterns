using System;

namespace BetterApproach.Strategies;

public interface INotificationStrategy
{
    void Send(Order order, string message);
}
