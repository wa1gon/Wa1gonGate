namespace Contacts.Views;

public partial class ContactsPage : ContentPage
{
    public ContactsPage()
    {
        InitializeComponent();

        List<Contact> contacts = new List<Contact>() {
            new Contact {Name = "John Doe", Email = "JohnDoe@gmail.com"},
            new Contact {Name = "Jane Doe", Email = "JaneDoe@gmail.com"},
            new Contact {Name = "Tom Hanks", Email = "TomHanks@gmail.com"},
            new Contact {Name = "Frank Liu", Email = "Frankliu@gmail.com"},
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

    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    private void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }
}