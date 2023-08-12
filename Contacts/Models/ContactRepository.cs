namespace Contacts.Models
{
    public static class ContactRepository
    {
        public static List<Contact> contacts = new List<Contact>() {
            new Contact {Name = "John Doe", Email = "JohnDoe@gmail.com"},
            new Contact {Name = "Jane Doe", Email = "JaneDoe@gmail.com"},
            new Contact {Name = "Tom Hanks", Email = "TomHanks@gmail.com"},
            new Contact {Name = "Frank Liu", Email = "Frankliu@gmail.com"},
        };

        public static List<Contact> GetContacts() => contacts;
        public static Contact GetContactById(int id)
        {
            return contacts.FirstOrDefault(x => x.ContactId == id);
        }

    }

}
