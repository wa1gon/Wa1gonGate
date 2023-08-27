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
