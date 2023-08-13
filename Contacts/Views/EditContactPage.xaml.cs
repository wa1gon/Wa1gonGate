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

            if (contact is null) return;
            //lblName.Text = contact.Name;
            contactControl.Name = contact.Name;
            contactControl.Email = contact.Email;
            contactControl.Phone = contact.Phone;
            contactControl.Address = contact.Address;
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {

        contact.Name = contactControl.Name;
        contact.Email = contactControl.Email;
        contact.Phone = contactControl.Phone;
        contact.Address = contactControl.Address;

        ContactRepository.UpdateContact(contact.ContactId, contact);
        Shell.Current.GoToAsync("..");
    }

    private void contactControl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "OK");
    }
}