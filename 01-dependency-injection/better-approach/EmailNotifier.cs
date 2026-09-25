namespace BetterApproach;

public class EmailNotifier
{
    public void Send(string toAddress, string message)
    {
        // Pretend this actually talks to an SMTP server.
        Console.WriteLine($"[EMAIL to {toAddress}] {message}");
    }
}
