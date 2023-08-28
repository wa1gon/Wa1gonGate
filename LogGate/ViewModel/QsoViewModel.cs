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
        private string lastMode = string.Empty;
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
                "8PSK125",
                "8PSK125F",
                "8PSK125FL",
                "8PSK250",
                "8PSK250F",
                "8PSK250FL",
                "8PSK500",
                "8PSK500F",
                "8PSK1000",
                "8PSK1000F",
                "8PSK1200F",
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
                "FSK31",
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
                "PSK10",
                "PSK31",
                "PSK63",
                "PSK63F",
                "PSK63RC4",
                "PSK63RC5",
                "PSK63RC10",
                "PSK63RC20",
                "PSK63RC32",
                "PSK125",
                "PSK125C12",
                "PSK125R",
                "PSK125RC10",
                "PSK125RC12",
                "PSK125RC16",
                "PSK125RC4",
                "PSK125RC5",
                "PSK250",
                "PSK250C6",
                "PSK250R",
                "PSK250RC2",
                "PSK250RC3",
                "PSK250RC5",
                "PSK250RC6",
                "PSK250RC7",
                "PSK500",
                "PSK500C2",
                "PSK500C4",
                "PSK500R",
                "PSK500RC2",
                "PSK500RC3",
                "PSK500RC4",
                "PSK800C2",
                "PSK800RC2",
                "PSK1000",
                "PSK1000C2",
                "PSK1000R",
                "PSK1000RC2",
                "PSKAM10",
                "PSKAM31",
                "PSKAM50",
                "PSKFEC31",
                "QPSK31",
                "QPSK63",
                "QPSK125",
                "QPSK250",
                "QPSK500",
                "ROS",
                "RTTY",
                "RTTYM",
                "SIM31",
                "SSB",
                "SSTV",
                "T10",
                "THOR",
                "THRB",
                "TOR",
                "WINMOR",
                "WSPR",
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
