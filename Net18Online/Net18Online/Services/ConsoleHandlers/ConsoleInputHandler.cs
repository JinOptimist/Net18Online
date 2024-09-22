using Net18Online.Models.Abstractions;

namespace Net18Online.Services.ConsoleHandlers;
internal class ConsoleInputHandler : IUserDataReceiver
{
    public string GetUserInput() =>
        Console.ReadLine() ?? string.Empty;
}