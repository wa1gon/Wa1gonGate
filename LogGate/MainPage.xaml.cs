namespace LogGate;

public partial class MainPage : ContentPage
{

    private QsoViewModel qsoVm;
    public MainPage(QsoViewModel mpvm)
    {

        InitializeComponent();
        qsoVm = mpvm;
        this.BindingContext = qsoVm;

    }
    private void ForceUppercase(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.Text = e.NewTextValue?.ToUpper();
        }
    }

    private void TimeClickEvent(object sender, FocusEventArgs e)
    {
        if (qsoVm.QsoTimeOnly.IsNullOrEmpty())
            qsoVm.QsoTimeOnly = DateTime.Now.ToString("t");
    }

    private void QslDateFocus(object sender, FocusEventArgs e)
    {
        if (qsoVm.QsoDate.IsNullOrEmpty())
            qsoVm.QsoDate = DateTime.Now.ToString("d");
    }
}

