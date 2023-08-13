namespace Contacts.Views.Controls;

public partial class ContactControl : ContentView
{
    public event EventHandler<string> OnError;
    public event EventHandler<EventArgs> OnSave;
    public event EventHandler<EventArgs> OnCancel;
    public string Email
    {
        get { return entryEmail.Text; }
        set { entryEmail.Text = value; }
    }
    public string Address
    {
        get { return entryAddress.Text; }
        set { entryAddress.Text = value; }
    }

    public string Name
    {
        get { return entryName.Text; }
        set { entryName.Text = value; }
    }

    public string Phone
    {
        get { return entryPhone.Text; }
        set { entryPhone.Text = value; }
    }

    public ContactControl()
    {
        InitializeComponent();
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsValid == false)
        {
            //DisplayAlert("Error", "Name is required.", "OK");
            OnError?.Invoke(sender, "Name is required.");
            return;
        }

        if (EmailValidator.IsValid == false)
        {
            foreach (var error in EmailValidator.Errors)
            {
                OnError?.Invoke(sender, error.ToString());
                //DisplayAlert("Error", error.ToString(), "OK");
            }
            return;
        }
        OnSave?.Invoke(sender, e);
        //contact.Name = entryName.Text;
        //contact.Email = entryEmail.Text;
        //contact.Phone = entryPhone.Text;
        //contact.Address = entryAddress.Text;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        OnSave?.Invoke(sender, e);
    }
}