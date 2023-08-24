namespace LogGate;

public partial class App : Application
{
    private static string key = "MjY2MTYwOEAzMjMyMmUzMDJlMzBsVU1hYTlhYTBRVkdRaVByRE9UZE1vTXNra0gxZEJjRm9vb1lrMVo2MmV3PQ==;MjY2MTYwOUAzMjMyMmUzMDJlMzBCeWJZMzJMeFZUaUhSMm5xTGZIVVhvV0pIc1JnNERNbEk5cGJFWWpJZEdFPQ==";

    public App()
	{
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);
        InitializeComponent();

		MainPage = new AppShell();
	}
}
