using Net18Online.Models.Abstractions;

namespace Net18Online.Models;
public class ComputerPlayer : Player
{
    private Random _random;

    public ComputerPlayer(int minNumber, int maxNumber) : base(minNumber, maxNumber)
    {
        _random = new Random();
    }

    public override int ChooseNumber() =>
        _random.Next(MinNumber, MaxNumber + 1);
}
