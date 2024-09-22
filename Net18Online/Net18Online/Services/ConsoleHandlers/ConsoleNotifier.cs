using Net18Online.Models.Abstractions;

namespace Net18Online.Services.ConsoleHandlers;
public class ConsoleNotifier : INotifier
{
    public void Inform(string message) =>
        ConsoleWriterUtil.PrintConsole(message, ConsoleColor.Cyan);

    public void Assist(string message) =>
        ConsoleWriterUtil.PrintConsole(message, ConsoleColor.Yellow);

    public void Compliment(string message)
    {
        ConsoleWriterUtil.PrintConsole(message, ConsoleColor.Green);
        Console.WriteLine();
    }

    public void Critical(string message)
    {
        ConsoleWriterUtil.PrintConsole(message, ConsoleColor.DarkRed);
        Console.WriteLine();
    }

    public void Major(string message) =>
        ConsoleWriterUtil.PrintConsole(message, ConsoleColor.Magenta);
}
