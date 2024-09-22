using Net18Online.Models;
using Net18Online.Models.Abstractions;

namespace Net18Online.Services;
public class MenuProvider
{
    private INotifier _notifier;
    private IUserDataReceiver _userDataReceiver;
    private GameMode[] _modes;

    public MenuProvider(INotifier notifier, IUserDataReceiver userDataReceiver)
    {
        _notifier = notifier;
        _userDataReceiver = userDataReceiver;
        _modes = (GameMode[])Enum.GetValues(typeof(GameMode));
    }

    public GameMode SelectMode()
    {
        Show();
        while (true)
        {
            if (int.TryParse(_userDataReceiver.GetUserInput(), out int mode))
            {
                if (mode >= (int)_modes.Min() && mode <= (int)_modes.Max())
                    return (GameMode)mode;
            }
            _notifier.Critical("Incorrect selection, please try again");
        }
    }

    private void Show()
    {
        _notifier.Inform(Environment.NewLine);
        _notifier.Inform("Please select the game mode:");
        Array.ForEach(_modes, m => _notifier.Inform($"{(int)m}. {m.GetDescription()}"));
    }
}
