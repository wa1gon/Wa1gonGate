using LogGate.ViewModel;

namespace LogGate.View;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        var foo = DeviceInfo.Platform;
    }

    private void ForceUppercase(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            entry.Text = e.NewTextValue.ToUpper();
        }
    }
    private void OnNumericEntryTextChanged(object sender, TextChangedEventArgs e)
    {

        if (sender is Entry entry)
        {
            entry.Text = FilterNumericInput(e.NewTextValue);
        }
    }


    private string FilterNumericInput(string input)
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            // Filter out non-numeric characters, except for decimal points
            string numericText = new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());

            // Remove extra decimal points
            int count = numericText.Count(c => c == '.');
            if (count > 1)
            {
                numericText = numericText.Replace(".", "");
            }

            return numericText;
        }
        return input;
    }

}