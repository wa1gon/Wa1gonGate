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
            entryName.Text = contact.Name;
            entryEmail.Text = contact.Email;
            entryPhone.Text = contact.Phone;
            entryAddress.Text = contact.Address;
        }
    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsValid == false)
        {
            DisplayAlert("Error", "Name is required.", "OK");
            return;
        }

        if (EmailValidator.IsValid == false)
        {
            foreach (var error in EmailValidator.Errors)
            {
                DisplayAlert("Error", error.ToString(), "OK");
            }
            return;
        }
        contact.Name = entryName.Text;
        contact.Email = entryEmail.Text;
        contact.Phone = entryPhone.Text;
        contact.Address = entryAddress.Text;

        ContactRepository.UpdateContact(contact.ContactId, contact);
        Shell.Current.GoToAsync("..");
    }
}