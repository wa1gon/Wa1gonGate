namespace LogGate.Services;

public class SettingServices
{
    public string DatabaseType { get; set; } = null;
    public string ConnectionString { get; set; } = null;
    public string Callsign { get; set; }
    public string GridSquare { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Initials { get; set; }
    public string State { get; set; }
    public string County { get; set; }
    public string Theme { get; set; }
    public string TelnetHost { get; set; }
    public string TelnetPort { get; set; }
    public string LoginCommand { get; set; }
}
