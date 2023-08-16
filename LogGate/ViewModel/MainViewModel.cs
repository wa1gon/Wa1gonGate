
namespace LogGate.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
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
        [RelayCommand]
        void Add()
        {
            //if (Text.IsNullOrEmpty())
            //    return;
            //items.Add(Text);
            //Text = string.Empty;
        }
    }
}
