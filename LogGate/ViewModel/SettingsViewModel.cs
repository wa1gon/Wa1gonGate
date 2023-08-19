using HamDotNetToolkit;
using LogGate.Model;
using Microsoft.Maui.Controls.Internals;

namespace LogGate.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public string? databaseType = "sqllocal";
    [ObservableProperty]
    public string? connectionString;
    [ObservableProperty]
    public string? callsign;
    [ObservableProperty]
    public string? gridSquare;
    [ObservableProperty]
    public decimal latitude;
    [ObservableProperty]
    public decimal longitude;
    [ObservableProperty]
    public string? initials;
    [ObservableProperty]
    public string? state;
    [ObservableProperty]
    public string? county;
    [ObservableProperty]
    public string? theme;
    [ObservableProperty]
    public string? telnetHost;
    [ObservableProperty]
    public string? telnetPort;
    [ObservableProperty]
    public string? loginCommand;

    public PickerOption DbSelection { get; set; }

    private SettingManager settingManager;
   

    public SettingsViewModel(SettingManager sm)
    {
          settingManager = sm;
        LoadSettingsIntoModel();
    }

    private void LoadSettingsIntoModel()
    {
        Callsign = settingManager.GetSetting(nameof(Callsign));
        DatabaseType = settingManager.GetSetting(nameof(DatabaseType));
        County = settingManager.GetSetting(nameof(County));
        GridSquare = settingManager.GetSetting(nameof(GridSquare));
        ConnectionString = settingManager.GetSetting(nameof(ConnectionString));

        Latitude = settingManager.GetSetting(nameof(Latitude)).ParseOrDefault<decimal>();

    }

    public List<PickerOption> DbOptions { get; set; } = new List<PickerOption>
        {
             new PickerOption {  Name="Sqlite", Value="sqlite"},
             new PickerOption {  Name="LocalDb SQL", Value="sqllocal"},
             //new PickerOption {  Name="Mongo DB", Value="mongo"},
             new PickerOption {  Name="SQL Server", Value="sqlserver"}
        };

    


    [RelayCommand]
    public void Close()
    {
        Shell.Current.GoToAsync("..");
    }
    [RelayCommand]
    public void Save()
    {
        settingManager.SaveSettings("LogGate.json");
    }
}
