namespace Contacts.Models
{
    public static class ContactRepository
    {
        public static List<Contact> contacts_ = new List<Contact>() {
            new Contact {ContactId =1, Name = "John Doe", Email = "JohnDoe@gmail.com"},
            new Contact {ContactId =2,Name = "Jane Doe", Email = "JaneDoe@gmail.com"},
            new Contact {ContactId =3,Name = "Tom Hanks", Email = "TomHanks@gmail.com"},
            new Contact {ContactId =4,Name = "Frank Liu", Email = "Frankliu@gmail.com"},
        };

        public static List<Contact> GetContacts() => contacts_;
        public static Contact GetContactById(int id)
        {
            var contact = contacts_.FirstOrDefault(x => x.ContactId == id);
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

            var contactToUpdate = contacts_.FirstOrDefault(x => x.ContactId == id);
            if (contactToUpdate is not null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Address = contact.Address;
                contactToUpdate.Phone = contact.Phone;
            }
        }
        public static void AddContact(Contact contact)
        {
            var maxId = contacts_.Max(x => x.ContactId);
            contact.ContactId = maxId + 1;
            contacts_.Add(contact);
        }
        public static void DeleteContact(int id)
        {
            var contact = contacts_.FirstOrDefault(x => x.ContactId == id);
            if (contact is not null)
            {
                contacts_.Remove(contact);
            }
        }

        internal static List<Contact> SearchContacts(string text)
        {
            var rc = contacts_.Where(x => !string.IsNullOrEmpty(x.Name) &&
                x.Name.ToUpper().StartsWith(text.ToUpper()))?.ToList();

            if (contacts_ is null || contacts_.Count <= 0)
                rc = contacts_.Where(x => !string.IsNullOrEmpty(x.Email) &&
                x.Email.ToUpper().StartsWith(text.ToUpper()))?.ToList();
            else return rc;

            if (contacts_ is null || contacts_.Count <= 0)
                rc = contacts_.Where(x => !string.IsNullOrEmpty(x.Phone.ToUpper()) &&
                x.Phone.StartsWith(text.ToUpper()))?.ToList();
            else return rc;

            if (contacts_ is null || contacts_.Count <= 0)
                rc = contacts_.Where(x => !string.IsNullOrEmpty(x.Address.ToUpper()) &&
                x.Address.StartsWith(text.ToUpper()))?.ToList();

            else return rc;

            return new List<Contact>();

        }
    }

}
