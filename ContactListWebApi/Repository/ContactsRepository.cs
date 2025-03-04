using ContactListWebApi.Data;
using ContactListWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApi.Repository;

public class ContactsRepository : IContactsRepository
{
    private readonly AppDbContext _context;
    public ContactsRepository(AppDbContext context) => _context = context;

    public async Task<List<Contact>> GetContactsAsync()
    {
        return await _context.Contacts.AsNoTracking().ToListAsync();
    }

    public async Task<Contact> GetContactByIdAsync(int id)
    {
        return await _context.Contacts
           .AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        var contactEntity = await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contactEntity.Entity;
    }

    public async Task DeleteContactAsync(int id)
    {
        var toDelete = await GetContactByIdAsync(id);
        _context.Contacts.Remove(toDelete);
        await _context.SaveChangesAsync();
    }



    public async Task<Contact> UpdateContactAsync(Contact contact)
    {
        var updated = _context.Update(contact);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }
}
