using Net18Online.Models;
using System.Text;
using System.Text.Json;

namespace Net18Online.Services;
public class SettingsProvider
{
    private string _settingsPath;

    public SettingsProvider(string settingsPath)
    {
        _settingsPath = settingsPath;
    }

    public GameSetting GetSetting()
    {
        var setting = LoadSetting();
        return setting;
    }

    private GameSetting LoadSetting()
    {
        if (!File.Exists(_settingsPath))
            return GetDefault();

        string json = File.ReadAllText(_settingsPath, Encoding.UTF8);
        GameSetting setting;
        try
        {
            setting = JsonSerializer.Deserialize<GameSetting>(json) ?? GetDefault();
            setting.CalculateAttempts();
        }
        catch (Exception ex)
        {
            setting = GetDefault();
            Console.WriteLine("Возникло исключение при чтении файлов настроек:" +
                $"{Environment.NewLine}{ex.Message}{Environment.NewLine}" +
                "Будут взяты настройки по умолчанию.");
        }
        return setting;
    }

    private GameSetting GetDefault() => new GameSetting();
}
