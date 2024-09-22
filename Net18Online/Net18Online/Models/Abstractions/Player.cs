namespace Net18Online.Models.Abstractions;
public abstract class Player : IPlay
{
    
    private INotifier? _notifier;
    private IUserDataReceiver? _receiver;

    public string Name { get; private set; }
    
    public int MinNumber { get; private set; }

    public int MaxNumber { get; private set; }

    public Player(GameSetting gameSetting, string name, INotifier? notifier = null, IUserDataReceiver? receiver = null)
    {
        Name = name;
        MinNumber = gameSetting.MinNumber;
        MaxNumber = gameSetting.MaxNumber;
        _notifier = notifier;
        _receiver = receiver;
    }

    public void UpdateRange(int min, int max)
    {
        if (min > max)
            return;
        MinNumber = min;
        MaxNumber = max;
    }

    protected virtual void Notify(string message) =>
        _notifier?.Inform(message);

    protected virtual string GetUserInput() =>
        _receiver?.GetUserInput() ?? throw new NotImplementedException();

    public abstract int ThinkANumber();

    public abstract int GuessANumber();
}