namespace BetterApproach;

public class ConsoleLogger: IAppLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}
