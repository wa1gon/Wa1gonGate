using LogGate.View;

namespace LogGate.ViewModel
{

    public partial class QsoViewModel : ObservableObject
    {
        [ObservableProperty]
        public string call = string.Empty;

        [ObservableProperty]
        public DateTime qsoDateOnly;

        [ObservableProperty]
        public DateTime qsoTimeOnly;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        public string mode = string.Empty;

        [ObservableProperty]
        public decimal freq = 0;

        [ObservableProperty]
        public ICollection<QsoDetail> qsoDetails = new List<QsoDetail>();

        public List<string> ModeList { get; set; } = new List<string>()
        {
                "AM",
                "ARDOP",
                "APRS",
                "ATV",
                "CLO",
                "CONTESTI",
                "CW",
                "DIGITALVOICE",
                "DOMINO",
                "DYNAMIC",
                "FAX",
                "FM",
                "FSK441",
                "FT4",
                "FT8",
                "HELL",
                "ISCAT",
                "JT6M",
                "JT44",
                "JT65",
                "JT9",
                "JS8Call",
                "MFSK",
                "MT63",
                "OLIVIA",
                "PAC",
                "PAX",
                "PKT",
                "PSK31",
                "PSK2K",
                // need more
                "Radar",
                "ROS",
                "RTTY",
                "SSTV",
                "TOR",
                "Throb",
                "Thor",
                "WSPR",
                "WSJT-X",
 
                // Add more modes here
            };
        private void ForceUppercase(object sender, TextChangedEventArgs e)
        {
            if (sender is Entry entry)
            {
                entry.Text = e.NewTextValue?.ToUpper();
            }
        }
        [RelayCommand]
        public void Add()
        {

            //if (Text.IsNullOrEmpty())
            //    return;
            //items.Add(Text);
            //Text = string.Empty;
        }
        [RelayCommand]
        public void Settings()
        {
            Shell.Current.GoToAsync(nameof(SettingsPage));
            //if (Text.IsNullOrEmpty())
            //    return;
            //items.Add(Text);
            //Text = string.Empty;
        }
    }
}
