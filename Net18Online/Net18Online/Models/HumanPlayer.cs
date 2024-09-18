using Net18Online.Models.Abstractions;
using Net18Online.Services;

namespace Net18Online.Models;
public class HumanPlayer : Player
{
    public HumanPlayer(int minNumber, int maxNumber) : base(minNumber, maxNumber)
    {
    }

    public override int ChooseNumber()
    {
        ConsoleWriterUtil.PrintConsoleInfo($"Select a number in the range from {MinNumber} to {MaxNumber}:");

        while (true)
        {
            var input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                if (number >= MinNumber && number <= MaxNumber)
                    return number;
            }
            ConsoleWriterUtil.PrintConsoleErr("The entered number does not meet the specified conditions, try again");
        }
    }
}