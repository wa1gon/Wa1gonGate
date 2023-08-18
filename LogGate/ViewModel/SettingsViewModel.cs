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

    [RelayCommand]
    public void Close()
    {
        Shell.Current.GoToAsync("..");
    }
}
