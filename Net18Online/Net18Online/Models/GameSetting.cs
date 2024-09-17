namespace Net18Online.Models;
public class GameSetting
{
    public int GuessAttempts { get; set; } = 5;
    public int MaxNumber { get; set; } = 50;
    public int MinNumber { get; set; } = 1;
}