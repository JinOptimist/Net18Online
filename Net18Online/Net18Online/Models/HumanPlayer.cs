using Net18Online.Models.Abstractions;

namespace Net18Online.Models;
public class HumanPlayer : Player
{
    public HumanPlayer(GameSetting gameSetting, string name, INotifier? notifier = null, IUserDataReceiver? receiver = null)
        : base(gameSetting, name, notifier, receiver)
    {
    }

    public override int GuessANumber() =>
        GetNumberFromConsoleInput();

    public override int ThinkANumber()
    {
        Notify($"Please, think a number in range from {MinNumber} to {MaxNumber}:");
        return GetNumberFromConsoleInput();
    }

    private int GetNumberFromConsoleInput()
    {
        while (true)
        {
            var input = GetUserInput();
            if (int.TryParse(input, out int number))
                if (number >= MinNumber && number <= MaxNumber)
                    return number;
            Notify("The entered number does not meet the specified conditions, try again");
        }
    }
}