namespace BadApproach;

public class ConsoleLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[LOG] {message}");
    }
}
