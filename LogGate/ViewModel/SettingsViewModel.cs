using LogGate.Model;

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
    public double formWidth;
    [ObservableProperty]
    public double formHeight;
    [ObservableProperty]
    public string rigCtldAddress;
    [ObservableProperty]
    public int rigCtldPort;
    [ObservableProperty]
    public int licensesKey;

    [ObservableProperty]
    public string dbSelection;

    [ObservableProperty]
    public DatabaseTypeModel? dbType;

    private SettingManager settingManager;


    public SettingsViewModel(SettingManager sm)
    {
        this.settingManager = sm;
        settingManager = sm;
        dbType = new();
        dbType.Name = string.Empty;
        dbType.Value = string.Empty;
        LoadSettingsIntoModel();
    }



    public List<DatabaseTypeModel> DbOptionType { get; set; } = new List<DatabaseTypeModel>
    {
        new DatabaseTypeModel() { Name="SQLite", Value="SqLite"},
        new DatabaseTypeModel() { Name="MicroSoft SQL (include LocalDB)", Value="MSSQL"},
        new DatabaseTypeModel() { Name="Mongo Database", Value="MongoDb"}
    };



    [RelayCommand]
    public void Close()
    {
        Shell.Current.GoToAsync("..");
    }

    private void LoadSettingsIntoModel()
    {
        MapSettingManagerToVM();

        void MapSettingManagerToVM()
        {
            Callsign = settingManager.GetSetting(nameof(Callsign));
            //DbSelection = settingManager.GetSetting(nameof(DatabaseType));
            var dbtype =settingManager.GetSetting(nameof(DatabaseType));
            DbType = DbOptionType.FirstOrDefault(x => x.Value == dbtype);
            ConnectionString = settingManager.GetSetting(nameof(ConnectionString));
            County = settingManager.GetSetting(nameof(County));
            State = settingManager.GetSetting(nameof(State));
            Initials = settingManager.GetSetting(nameof(Initials));
            Theme = settingManager.GetSetting(nameof(Theme));
            TelnetHost = settingManager.GetSetting(nameof(TelnetHost));
            TelnetPort = settingManager.GetSetting(nameof(TelnetPort)).ParseOrDefault<int>();
            LoginCommand = settingManager.GetSetting(nameof(LoginCommand));
            FormWidth = settingManager.GetSetting(nameof(FormWidth)).ParseOrDefault<double>();
            FormHeight = settingManager.GetSetting(nameof(FormHeight)).ParseOrDefault<double>();


            ConnectionString = settingManager.GetSetting(nameof(ConnectionString));

            GridSquare = settingManager.GetSetting(nameof(GridSquare));
            Latitude = settingManager.GetSetting(nameof(Latitude)).ParseOrDefault<decimal>();
            Longitude = settingManager.GetSetting(nameof(Longitude)).ParseOrDefault<decimal>();

            Operator = settingManager.GetSetting(nameof(Operator));
            RigCtldPort = settingManager.GetSetting(nameof(RigCtldPort)).ParseOrDefault<int>();
            RigCtldAddress = settingManager.GetSetting(nameof(RigCtldAddress));
        }

    }
    [RelayCommand]
    public void Save()
    {
        MapModelToSettingManager();

        settingManager.SaveSettings("LogGate.json");

        void MapModelToSettingManager()
        {
            settingManager.SetSetting(nameof(Callsign), Callsign);
            settingManager.SetSetting(nameof(DatabaseType), dbType.Value);
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
            settingManager.SetSetting(nameof(FormHeight), FormHeight);
            settingManager.SetSetting(nameof(FormWidth), FormWidth);
            settingManager.SetSetting(nameof(RigCtldAddress), RigCtldAddress);
            settingManager.SetSetting(nameof(RigCtldPort), RigCtldPort);
        }
    }
}
