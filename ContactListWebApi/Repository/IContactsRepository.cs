using ContactListWebApi.Models;

namespace ContactListWebApi.Repository;

public interface IContactsRepository
{
    Task<List<Contact>> GetContactsAsync();
    Task<Contact> GetContactByIdAsync(int id);

    Task<Contact> CreateContactAsync(Contact contact);
    Task<Contact> UpdateContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
}
