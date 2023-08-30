namespace LogGate;

public partial class App : Application
{
    private static string key = "MjY2MTYwOEAzMjMyMmUzMDJlMzBsVU1hYTlhYTBRVkdRaVByRE9UZE1vTXNra" +
        "0gxZEJjRm9vb1lrMVo2MmV3PQ==;MjY2MTYwOUAzMjMyMmUz" +
        "MDJlMzBCeWJZMzJMeFZUaUhSMm5xTGZIVVhvV0pIc1JnNERNbEk5cGJFWWpJZEdFPQ==";
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
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        const int newWidth = 900;
        const int newHeight = 800;

        window.X = 100;
        window.Y = 200;

        window.Width = newWidth;
        window.Height = newHeight;

        window.MinimumWidth = newWidth;
        window.MinimumHeight = newHeight;
        return window;
    }
}
