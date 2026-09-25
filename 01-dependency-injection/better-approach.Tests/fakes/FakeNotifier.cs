using System;

namespace BetterApproach.Tests.fakes;

public class FakeNotifier : INotifier
{
    // A list (not a Dictionary) because a fake should record every call,
    // even if the same recipient is notified more than once - Dictionary.Add
    // would throw on a duplicate key instead of just recording it.
    public List<(string ToAddress, string Message)> Notifications { get; } = new();

    public void Notify(string toAddress, string message)
    {
        Notifications.Add((toAddress, message));
    }
}
