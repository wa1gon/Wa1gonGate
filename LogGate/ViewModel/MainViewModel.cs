


using CommunityToolkit.Mvvm.ComponentModel;

namespace LogGate.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        Qso qso;
        [ObservableProperty]
        string text;

        void Add()
        {
            text = string.Empty;
        }
    }
}
