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
