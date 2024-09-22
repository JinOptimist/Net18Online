using Net18Online.Models.Abstractions;

namespace Net18Online.Models;
public class ComputerPlayer : Player
{
    private Random _random;

    public ComputerPlayer(GameSetting gameSetting, string name) : base(gameSetting, name)
    {
        _random = new Random();
    }

    public override int GuessANumber() =>
        (MaxNumber - MinNumber) / 2 + MinNumber;

    public override int ThinkANumber() =>
        _random.Next(MinNumber, MaxNumber + 1);
}
