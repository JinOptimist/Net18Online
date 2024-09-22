using Net18Online.Models;
using Net18Online.Models.Abstractions;

namespace Net18Online.Services;
public class GameManager
{
    private INotifier _notifier;
    private IUserDataReceiver _userDataReceiver;
    private MenuProvider _menu;
    private GameSetting _settings;
    private const string FirstPlayerIdentifier = "first";
    private const string SecondPlayerIdentifier = "second";

    public GameManager(INotifier notifier, IUserDataReceiver userDataReceiver)
    {
        _notifier = notifier;
        _userDataReceiver = userDataReceiver;
        _menu = new MenuProvider(_notifier, _userDataReceiver);
        _settings = new SettingsProvider(Path.Combine("Configuration", "config.json")).GetSetting();
    }
    private void Greeting()
    {
        _notifier.Major($"Hello, {Environment.UserName}!");
        _notifier.Major("You are welcomed by the manager of the \"Guess the number game\"!");
    }

    public Game BuildGame()
    {
        Greeting();
        var mode = _menu.SelectMode();

        Player firstPlayer;
        Player secondPlayer;
        switch (mode)
        {
            case GameMode.PlayerVsComputer:
                firstPlayer = new ComputerPlayer(_settings, GetComputerPlayerName());
                secondPlayer = new HumanPlayer(_settings, GetHumanPlayerName(), _notifier, _userDataReceiver);
                break;
            case GameMode.PlayerVsPlayer:
                firstPlayer = new HumanPlayer(_settings, GetHumanPlayerName(FirstPlayerIdentifier), _notifier, _userDataReceiver);
                secondPlayer = new HumanPlayer(_settings, GetHumanPlayerName(SecondPlayerIdentifier), _notifier, _userDataReceiver);
                break;
            case GameMode.ComputerVsComputer:
                firstPlayer = new ComputerPlayer(_settings, GetComputerPlayerName(FirstPlayerIdentifier));
                secondPlayer = new ComputerPlayer(_settings, GetComputerPlayerName(SecondPlayerIdentifier));
                break;
            default:
                throw new ArgumentException("Unknow game mode selected");
        }
        return new Game(firstPlayer, secondPlayer, _settings, _notifier);
    }

    private string GetComputerPlayerName(string? playerIdentifier = null) =>
        "Bot player" +
        (string.IsNullOrEmpty(playerIdentifier) ? string.Empty : $"({playerIdentifier})");

    private string GetHumanPlayerName(string? playerIdentifier = null)
    {
        var message = playerIdentifier == null ?
            "Please, enter human player name:" :
            $"Please, enter {playerIdentifier} human player name:";
        
        _notifier.Assist(message);
        var name = _userDataReceiver.GetUserInput();

        if (!string.IsNullOrEmpty(name))
            return name;

        if (string.IsNullOrEmpty(playerIdentifier))
            return nameof(HumanPlayer);
        return $"{nameof(HumanPlayer)} ({playerIdentifier})";
    }
}
