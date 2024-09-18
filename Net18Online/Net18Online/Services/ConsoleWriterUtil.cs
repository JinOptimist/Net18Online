namespace Net18Online.Services;
public class ConsoleWriterUtil
{
    /// <summary>
    /// Printing text to the console to indicate an action
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    public static void PrintConsoleInfo(string text) => PrintConsole(text, ConsoleColor.Cyan);

    /// <summary>
    /// Printing text to the console with successful completion
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    public static void PrintConsoleWin(string text) => PrintConsole(text, ConsoleColor.Green);

    /// <summary>
    /// Printing hint text to the console
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    public static void PrintConsoleHint(string text) => PrintConsole(text, ConsoleColor.Yellow);

    /// <summary>
    /// Printing text to the console with a loss
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    public static void PrintConsoleLoss(string text) => PrintConsole(text, ConsoleColor.DarkRed);

    /// <summary>
    /// Printing text to the console to indicate an input error
    /// </summary>
    /// <param name="text">The text that will be displayed on the console</param>
    public static void PrintConsoleErr(string text) => PrintConsole(text, ConsoleColor.Magenta);

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