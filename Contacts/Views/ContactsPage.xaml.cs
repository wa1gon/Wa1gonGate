namespace Contacts.Views;

public partial class ContactsPage : ContentPage
{
    public ContactsPage()
    {
        InitializeComponent();

        List<string> contacts = new List<string>() {
        "John Doe",
        "Jane Doe",
        "Tom Hanks",
        "Frank Liu"
        };

        listContacts.ItemsSource = contacts;
    }

    private void btnEditContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(EditContactPage));
    }

    private void btnAddContact_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AddContactPage));
    }
}