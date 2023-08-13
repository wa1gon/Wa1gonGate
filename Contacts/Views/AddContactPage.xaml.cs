using Contacts.Models;

namespace Contacts.Views;

public partial class AddContactPage : ContentPage
{
    public AddContactPage()
    {
        InitializeComponent();
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnSave(object sender, EventArgs e)
    {
        ContactRepository.AddContact(new Models.Contact
        {
            Name = contactControl.Name,
            Email = contactControl.Email,
            Address = contactControl.Address,
            Phone = contactControl.Phone
        });
        Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }

}