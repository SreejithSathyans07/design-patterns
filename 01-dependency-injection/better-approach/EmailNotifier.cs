namespace BetterApproach;

public class EmailNotifier: INotifier
{
    public void Notify(string toAddress, string message)
    {
        // Pretend this actually talks to an SMTP server.
        Console.WriteLine($"[EMAIL to {toAddress}] {message}");
    }
}
