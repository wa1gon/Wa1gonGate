namespace LogGate.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        public string? databaseType;
        public string? connectionString;
        public string? callsign;
        public string? gridSquare;
        public decimal latitude;
        public decimal longitude;
        public string? initials;
        public string? state;
        public string? county;
        public string? theme;
        public string? telnetHost;
        public string? telnetPort;
        public string? loginCommand;
    }
}
