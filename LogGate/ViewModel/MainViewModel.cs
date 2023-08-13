
namespace LogGate.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        ObservableCollection<string> items;

        [ObservableProperty]
        Qso qso;
        [ObservableProperty]
        string text;


        public MainViewModel()
        {
            items = new ObservableCollection<string>();
        }
        [RelayCommand]
        void Add()
        {
            if (Text.IsNullOrEmpty())
                return;
            items.Add(Text);
            Text = string.Empty;
        }
    }
}
