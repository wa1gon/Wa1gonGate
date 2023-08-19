namespace LogGate.Model
{
    /// <summary>
    /// I think this is not going to be used.
    /// </summary>
    public class Settings
    {
        public string? Callsign { get; set; }
        public string? DatabaseType { get; set; } = null;
        public string? ConnectionString { get; set; } = null;

        public string? GridSquare { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Initials { get; set; }
        public string? State { get; set; }
        public string? County { get; set; }
        public string? Theme { get; set; }
        public string? TelnetHost { get; set; }
        public string? TelnetPort { get; set; }
        public string? LoginCommand { get; set; }

    }
}
