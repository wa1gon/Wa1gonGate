using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

public class SettingManager
{
    private readonly string _fileName;
    private Dictionary<string, string> settings;

    public SettingManager(string? fileName)
    {
        _fileName = fileName;
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (File.Exists(_fileName))
        {
            string json = File.ReadAllText(_fileName);
            settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
        else
        {
            settings = new Dictionary<string, string>();
        }
    }

    private void SaveSettings()
    {
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(_fileName, json);
    }

    public string? GetSetting(string key)
    {
        if (settings.TryGetValue(key, out string value))
        {
            return value;
        }
        return null; // Setting not found
    }

    public void SetSetting(string key, string value)
    {
        if (settings.ContainsKey(key))
        {
            settings[key] = value;
        }
        else
        {
            settings.Add(key, value);
        }
        SaveSettings();
    }
}
