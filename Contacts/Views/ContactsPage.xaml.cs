namespace Contacts.Views;
using Contacts.Models;
public partial class ContactsPage : ContentPage
{
    public ContactsPage()
    {
        InitializeComponent();
        List<Contact> contacts = ContactRepository.GetContacts();
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



    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (listContacts.SelectedItem != null)
            await Shell.Current.GoToAsync(nameof(EditContactPage));
        //DisplayAlert("test", "test", "OK");
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        listContacts.SelectedItem = null;
    }
}