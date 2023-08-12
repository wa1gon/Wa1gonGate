namespace Contacts.Models
{
    public static class ContactRepository
    {
        public static List<Contact> contacts = new List<Contact>() {
            new Contact {ContactId =1, Name = "John Doe", Email = "JohnDoe@gmail.com"},
            new Contact {ContactId =2,Name = "Jane Doe", Email = "JaneDoe@gmail.com"},
            new Contact {ContactId =3,Name = "Tom Hanks", Email = "TomHanks@gmail.com"},
            new Contact {ContactId =4,Name = "Frank Liu", Email = "Frankliu@gmail.com"},
        };

        public static List<Contact> GetContacts() => contacts;
        public static Contact GetContactById(int id)
        {
            var contact = contacts.FirstOrDefault(x => x.ContactId == id);
            if (contact is null) return null;

            return new Contact
            {
                ContactId = contact.ContactId,
                Name = contact.Name,
                Email = contact.Email,
                Address = contact.Address,
                Phone = contact.Phone
            };
        }
        public static void UpdateContact(int id, Contact contact)
        {
            if (id != contact.ContactId) return;

            var contactToUpdate = contacts.FirstOrDefault(x => x.ContactId == id);
            if (contactToUpdate is not null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
            }
        }

    }

}
