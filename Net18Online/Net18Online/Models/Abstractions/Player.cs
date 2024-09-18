namespace Net18Online.Models.Abstractions;
public abstract class Player : IChoosable
{
    public int MinNumber { get; }

    public int MaxNumber { get; }

    public Player(int minNumber, int maxNumber)
    {
        MinNumber = minNumber;
        MaxNumber = maxNumber;
    }
    
    public abstract int ChooseNumber();
}