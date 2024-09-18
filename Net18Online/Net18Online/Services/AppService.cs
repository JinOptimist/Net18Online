using Net18Online.Models;

namespace Net18Online.Services;
public class AppService
{
    private string[] _args;

    public AppService(string[] args)
    {
        _args = args;
    }

    public void Run()
    {
        var setting = new SettingService(Path.Combine("Configuration", "config.json"), _args).GetSetting();

        var robot = new ComputerPlayer(setting.MinNumber, setting.MaxNumber);
        var human = new HumanPlayer(setting.MinNumber, setting.MaxNumber);

        var gm = new GameManager(robot, human, setting);
        while (true)
        {
            gm.Start();
            Console.WriteLine("Do you want to play again? (Y/N)");
            if (Console.ReadLine()?.ToLower() != "y")
                break;
        }
    }
}
