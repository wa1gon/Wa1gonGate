namespace LogGate;

public partial class MainPage : ContentPage
{

    private QsoViewModel qsoVm;
    private SettingManager sm;
    public MainPage(QsoViewModel mpvm, IQSORepo repo, SettingManager sm)
    {

        InitializeComponent();
        this.sm = sm;
        qsoVm = mpvm;
        this.BindingContext = qsoVm;
        qsoVm.StartThread();
        qsoVm.repo = repo;
        qsoVm.repo.CreateContext(sm.GetSetting(SettingManager.ConnectionString));
        qsoVm.GetAllQsos();

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

