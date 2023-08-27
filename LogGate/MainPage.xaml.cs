using LogGate.ViewModel;


namespace LogGate;

public partial class MainPage : ContentPage
{

    private MainViewModel mainPageVM;
    public MainPage(MainViewModel mpvm)
    {

        InitializeComponent();
        mainPageVM = mpvm;

    }
    private void ForceUppercase(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.Text = e.NewTextValue?.ToUpper();
        }
    }
}

