using System;

namespace BetterApproach.Tests.fakes;

public class FakeLogger : IAppLogger
{
    public List<string> logMessages = new();
    public void Log(string message)
    {
        logMessages.Add(message);
    }
}
