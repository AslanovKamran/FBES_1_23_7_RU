using ContactListWebApi.Data;
using ContactListWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactListWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContactsController(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetContacts()
    {
        return new JsonResult(_context.Contacts.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<Contact> GetContactById(int id)
    {
        var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
        return contact!;
    }

    [HttpPost]
    public void CreateContact(Contact contact)
    {
        var entity = _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    [HttpPost("bulk")]
    public void BulkInser(List<Contact> contacts)
    {
        _context.Contacts.AddRange(contacts);
        _context.SaveChanges();
    }

    [HttpPut]
    public void UpdateContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }

    [HttpDelete("{id}")]
    public void DeleteContactById(int id)
    {
        _context.Remove(_context.Contacts.FirstOrDefault(c => c.Id == id));
        _context.SaveChanges();
    }
}
