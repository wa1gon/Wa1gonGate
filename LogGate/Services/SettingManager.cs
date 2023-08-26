namespace HamDotNetToolkit;

using LogGate;
using Newtonsoft.Json;
using System.Diagnostics;
using Formatting = Newtonsoft.Json.Formatting;


/// <summary>
/// Setting was designed to be a DI service
/// </summary>
public class SettingManager
{
    private readonly string? fileName;
    private Dictionary<string, string> settings = new();

    public const string Call = "Call";
    public const string Country = "Country";
    public const string Continent = "Continent";
    public const string Lat = "Lat";
    public const string Long = "Long";
    public const string Operator = "Operator";
    public const string Initials = "Initials";
    public const string Grid = "Grid";
    public const string State = "State";
    public const string County = "County";
    public const string FormWidth = "FormWidth";
    public const string FormHeight = "FormHeight";
    public const string DatabaseType = "DatabaseType"; // local,sql, sqlite, databaseGate
    public const string ConnectionString = "ConnectionString";

    public SettingManager()
    {
        LoadSettings("LogGate.json");
    }
    public void LoadSettings(string fileName)
    {
        try
        {
            fileName = MauiHelper.GetSettingsFilePath(fileName);
            Debug.WriteLine(fileName);
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
    }

    public void SaveSettings(string fileName)
    {
        fileName = MauiHelper.GetSettingsFilePath(fileName);
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        fileName = MauiHelper.GetSettingsFilePath(fileName);
        File.WriteAllText(fileName, json);
    }

    public string GetSetting(string key)
    {
        if (settings.TryGetValue(key, out string? value))
        {
            return value;
        }
        return string.Empty; // Setting not found
    }

    public void SetSetting(string key, string value)
    {
        if (value.IsNullOrEmpty())
            return;

        settings[key] = value;
    }

    public void SetSetting(string key, decimal value)
    {
        settings[key] = value.ToString();
    }
    public void SetSetting(string key, int value)
    {
        settings[key] = value.ToString();
    }
    public void SetSetting(string key, long value)
    {
        settings[key] = value.ToString();
    }
}
