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
        private string name;

        [ObservableProperty]
        public string mode = string.Empty;

        [ObservableProperty]
        public ICollection<QsoDetail> qsoDetails = new List<QsoDetail>();


        [RelayCommand(CanExecute = nameof(CanAdd))]
        public void Add()
        {
            //if (Text.IsNullOrEmpty())
            //    return;
            //items.Add(Text);
            //Text = string.Empty;
        }

        private bool CanAdd()
        {
            return true;
        }
    }
}
