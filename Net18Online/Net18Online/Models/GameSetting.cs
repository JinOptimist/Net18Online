namespace Net18Online.Models;
public class GameSetting
{
    public int GuessAttempts { get; set; } = 5;
    
    public int MaxNumber { get; set; } = 50;
    
    public int MinNumber { get; set; } = 1;
    
    public bool IsAutoSetAttempts { get; set; } = true;
    
    public bool IsUpdateRanges { get; set; } = true;
    
    public bool IsEnableHints { get; set; } = true;

    public GameSetting()
    {
        CalculateAttempts();
    }

    public void CalculateAttempts()
    {
        if (IsAutoSetAttempts)
            GuessAttempts = (int)Math.Ceiling(Math.Log2(MaxNumber));
    }
}