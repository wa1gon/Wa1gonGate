namespace LogGate;

public partial class App : Application
{
    private static string key = "MjY2MTYwOEAzMjMyMmUzMDJlMzBsVU1hYTlhYTBRVkdRaVByRE9UZE1vTXNra0gxZEJjRm9vb1lrMVo2MmV3PQ==;MjY2MTYwOUAzMjMyMmUzMDJlMzBCeWJZMzJMeFZUaUhSMm5xTGZIVVhvV0pIc1JnNERNbEk5cGJFWWpJZEdFPQ==";
    private SettingManager sm { get; set; }
    private IQSORepo QslRepo { get; set; }
    public App(SettingManager sm, IQSORepo repo)
    {
        this.sm = sm;
        QslRepo = repo;
        OpenDatabase();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);
        InitializeComponent();

        MainPage = new AppShell();
    }

    private void OpenDatabase()
    {
        string dbName = sm.GetSetting(SettingManager.DatabaseType);
        string connection = sm.GetSetting(SettingManager.ConnectionString);


    }
}
