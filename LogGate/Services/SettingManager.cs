namespace HamDotNetToolkit;

using LogGate;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;


/// <summary>
/// Setting was designed to be a DI service
/// </summary>
public class SettingManager
{
    private readonly string? fileName;
    private Dictionary<string, string> settings = new();


    public void LoadSettings(string fileName)
    {
        try
        {
            fileName = MauiHelper.GetSettingsFilePath(fileName);
            if (File.Exists(fileName))
            {

                string json = File.ReadAllText(fileName);
                if (json.IsNullOrEmpty()) return;

                settings = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                settings = new Dictionary<string, string>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"File error: {ex.Message}");
        }
        finally
        {

        }
    }

    public void SaveSettings(string fileName)
    {
        fileName = MauiHelper.GetSettingsFilePath(fileName);
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        fileName = MauiHelper.GetSettingsFilePath(fileName);
        File.WriteAllText(fileName, json);
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

    }
}
