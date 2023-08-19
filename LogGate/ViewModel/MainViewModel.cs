namespace LogGate.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        SettingManager settingManagerVM;
        public MainViewModel(SettingManager sm)
        {
            settingManagerVM = sm;
            settingManagerVM.LoadSettings("logGate.json");

            // testing
            settingManagerVM.SetSetting("call", "WA1GON");
            settingManagerVM.SaveSettings("logGate.json");
        }
        ObservableCollection<string> items;

        [ObservableProperty]
        private string call = string.Empty;

        [ObservableProperty]
        DateTime qsoDateOnly;


        [ObservableProperty]
        DateTime qsoTimeOnly;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string mode = string.Empty;

        [ObservableProperty]
        private ICollection<QsoDetail> qsoDetails = new List<QsoDetail>();


        public MainViewModel()
        {
            items = new ObservableCollection<string>();
        }
    }
}
