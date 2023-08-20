﻿using HamDotNetToolkit;
using LogGate.Model;
using Microsoft.Maui.Controls.Internals;

namespace LogGate.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public string? databaseType = string.Empty;
    [ObservableProperty]
    public string connectionString = string.Empty;
    [ObservableProperty]
    public string callsign = string.Empty;
    [ObservableProperty]
    public string gridSquare = string.Empty;
    [ObservableProperty]
    public decimal latitude;
    [ObservableProperty]
    public decimal longitude;
    [ObservableProperty]
    public string initials = string.Empty;
    [ObservableProperty]
    public string state = string.Empty;
    [ObservableProperty]
    public string county = string.Empty;
    [ObservableProperty]
    public string theme = string.Empty;
    [ObservableProperty]
    public string telnetHost = string.Empty;
    [ObservableProperty]
    public int telnetPort;
    [ObservableProperty]
    public string loginCommand = string.Empty;
    [ObservableProperty]
    public string _operator = string.Empty;
    [ObservableProperty]
    public int formWidth;
    [ObservableProperty]
    public int formHight;


    public PickerOption DbSelection { get; set; } = new();

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
        DbSelection.Value = databaseType;

        ConnectionString = settingManager.GetSetting(nameof(ConnectionString));
        County = settingManager.GetSetting(nameof(County));
        State = settingManager.GetSetting(nameof(State));
        Initials = settingManager.GetSetting(nameof(Initials));
        Theme = settingManager.GetSetting(nameof(Theme));
        TelnetHost = settingManager.GetSetting(nameof(TelnetHost));
        TelnetPort = settingManager.GetSetting(nameof(TelnetPort)).ParseOrDefault<int>();
        LoginCommand = settingManager.GetSetting(nameof(LoginCommand));
       

        ConnectionString = settingManager.GetSetting(nameof(ConnectionString));

        GridSquare = settingManager.GetSetting(nameof(GridSquare));
        Latitude = settingManager.GetSetting(nameof(Latitude)).ParseOrDefault<decimal>();
        Longitude = settingManager.GetSetting(nameof(Longitude)).ParseOrDefault<decimal>();

        Operator = settingManager.GetSetting(nameof(Operator));

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
        settingManager.SetSetting(nameof(Callsign), Callsign);
        settingManager.SetSetting(nameof(DatabaseType), DbSelection.Value);
        settingManager.SetSetting(nameof(ConnectionString), ConnectionString);
        settingManager.SetSetting(nameof(GridSquare), GridSquare);
        settingManager.SetSetting(nameof(Latitude), Latitude);
        settingManager.SetSetting(nameof(Longitude), Longitude);
        settingManager.SetSetting(nameof(Initials), Initials);
        settingManager.SetSetting(nameof(State), State);
        settingManager.SetSetting(nameof(County), County);
        settingManager.SetSetting(nameof(Theme), Theme);
        settingManager.SetSetting(nameof(TelnetHost), TelnetHost);
        settingManager.SetSetting(nameof(TelnetPort), TelnetPort);
        settingManager.SetSetting(nameof(LoginCommand), LoginCommand);
        settingManager.SetSetting(nameof(Operator), Operator);
        settingManager.SetSetting(nameof(FormHight), FormHight);
        settingManager.SetSetting(nameof(FormWidth), FormWidth);


        settingManager.SaveSettings("LogGate.json");
    }
}
