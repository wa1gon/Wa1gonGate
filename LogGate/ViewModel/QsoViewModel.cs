namespace LogGate.ViewModel
{
    [INotifyPropertyChanged]
    public partial class QsoViewModel
    {
        [ObservableProperty]
        private string call = string.Empty;

        [ObservableProperty]
        DateTime qsoDateOnly;

        [ObservableProperty]
        DateTime qsoTimeOnly;


        [ObservableProperty]
        private DateTime timeOn;
        [ObservableProperty]
        private string mode = string.Empty;

        [ObservableProperty]
        private ICollection<QsoDetail> qsoDetails = new List<QsoDetail>();
        private DateTime _qsoDate;

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
