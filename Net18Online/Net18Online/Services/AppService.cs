using Net18Online.Models;
using Net18Online.Services.ConsoleHandlers;

namespace Net18Online.Services;
public class AppService
{
    public void Run()
    {
        var notifier = new ConsoleNotifier();
        var inputReceiver = new ConsoleInputHandler();
        var gm = new GameManager(notifier, inputReceiver);
        var game = gm.BuildGame();

        while (true)
        {
            game.Play();
            notifier.Inform("Do you want to play again? (Y/N)");
            if (inputReceiver.GetUserInput().ToLower() != "y")
                break;
        }
    }
}