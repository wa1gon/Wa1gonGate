namespace Contacts.Views;
using Contacts.Models;
[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage : ContentPage
{
    public Contact contact { get; set; }
    public EditContactPage()
    {
        InitializeComponent();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }
    public string ContactId
    {
        set
        {
            contact = ContactRepository.GetContactById(int.Parse(value));
            lblName.Text = contact.Name;
        }
    }
}