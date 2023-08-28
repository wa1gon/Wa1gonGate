using LogGate.ViewModel;
using System.Text.RegularExpressions;

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
            entry.Text = e.NewTextValue?.ToUpper();
        }
    }
    private void FixGridSquare(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.IsNullOrEmpty() == true) return;

        if (sender is Entry entry)
        {
            // Remove non-alphanumeric characters
            string cleanedInput = Regex.Replace(e.NewTextValue, "[^a-zA-Z0-9]", "");

            // Convert to uppercase
            string upperCaseInput = cleanedInput.ToUpper();

            // Format as grid square (example: EF42ab)
            string firstPart = upperCaseInput.Length >= 2 ? upperCaseInput.Substring(0, 2) : "";
            string secondPart = upperCaseInput.Length >= 4 ? upperCaseInput.Substring(2, 2) : "";
            string thirdPart = upperCaseInput.Length >= 6 ? upperCaseInput.Substring(4, Math.Min(2, upperCaseInput.Length - 4)).ToLower() : "";
            string gridSquare = $"{firstPart}{secondPart}{thirdPart}";

            entry.Text = gridSquare;
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