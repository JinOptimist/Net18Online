namespace Net18Online.Services.ConsoleHandlers;
public class ConsoleWriterUtil
{
    /// <summary>
    /// Printing arbitrary text to the console
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    /// <param name="color">The color that the message will be printed in the console</param>
    public static void PrintConsole(string text, ConsoleColor color)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = currentColor;
    }
}