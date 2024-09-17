using Net18Online.Models;
using System.Text;
using System.Text.Json;

namespace Net18Online.Services;
public class SettingService
{
    private string _settingsPath;
    private string[] _args;

    public SettingService(string settingsPath, string[] args)
    {
        _settingsPath = settingsPath;
        _args = args;
    }

    public GameSetting GetSetting()
    {
        GameSetting setting = LoadSetting();

        var settingsMap = GetArgsSettings();
        if (settingsMap.Count() != 0)
        {
            if (settingsMap.ContainsKey(nameof(GameSetting.GuessAttempts)))
                setting.GuessAttempts = settingsMap[nameof(GameSetting.GuessAttempts)];

            if (settingsMap.ContainsKey(nameof(GameSetting.MaxNumber)))
                setting.MaxNumber = settingsMap[nameof(GameSetting.MaxNumber)];

            if (settingsMap.ContainsKey(nameof(GameSetting.MaxNumber)))
                setting.MinNumber = settingsMap[nameof(GameSetting.MaxNumber)];
        }
        if (CheckIsNeedSaveSetting(settingsMap))
            SaveSetting(setting);
        return setting;
    }

    private Dictionary<string, int> GetArgsSettings()
    {
        var map = new Dictionary<string, int>();
        if (_args is null || _args.Length == 0 || _args[0] != "-configure")
            return map;
        for (int i = 1; i < _args.Length; i++)
        {
            AddArgumentSetting(nameof(GameSetting.GuessAttempts), i, map);
            AddArgumentSetting(nameof(GameSetting.MaxNumber), i, map);
            AddArgumentSetting(nameof(GameSetting.MinNumber), i, map);
        }
        return map;
    }

    private void AddArgumentSetting(string settingName, int index, Dictionary<string, int> map)
    {
        var argSeparator = '=';
        if (_args[index].ToLower().StartsWith($"-{settingName.ToLower()}") && _args[index].Contains(argSeparator))
        {
            var splittedArg = _args[index].Split(argSeparator);
            if (int.TryParse(splittedArg[1], out int argument))
                map.Add(settingName, argument);
        }
    }

    private bool CheckIsNeedSaveSetting(Dictionary<string, int> map)
    {
        bool isSave = false;
        if (map.Count > 0)
            isSave = _args.Contains("-save");
        return isSave;
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

    private void SaveSetting(GameSetting setting)
    {
        try
        {
            string json = JsonSerializer.Serialize(setting, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"При попытке перезаписи настроек в файл '{_settingsPath}' возникло исключение:" +
                $"{Environment.NewLine}{ex.Message}{Environment.NewLine}" +
                "Новые настройки не будут сохранены");
        }
    }

    private GameSetting GetDefault() =>
        new GameSetting();
}
