using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ContactListWebApi.Models;
using ContactListWebApi.Data;

namespace ContactListWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ContactsController(AppDbContext context) => _context = context;

    #region Get 

    /// <summary>
    /// Get all contacts
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await _context.Contacts
            .AsNoTracking().ToListAsync();

        if (!contacts.Any())
            return NoContent(); 

        return Ok(contacts);
    }

    /// <summary>
    /// Get an existing contact by id   
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet("{id}")]
    public IActionResult GetContactById(int id)
    {
        var contact = _context.Contacts
            .AsNoTracking().FirstOrDefault(c=>c.Id == id);

        if (contact is null)
            return NotFound($"Contact with ID = {id} was not found.");

        return Ok(contact);
    }

    #endregion

    #region Post

    /// <summary>
    /// Create a new contact
    /// </summary>
    /// <param name="contact"></param>
    /// <returns></returns>

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreateContact(Contact contact)
    {
        if (contact is null)
            return BadRequest("Contact cannot be null.");

        var contactEntity = _context.Contacts.Add(contact).Entity;
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetContactById), 
                               new { id = contactEntity.Id }, 
                               contactEntity);
    }

    #endregion

    #region Update
    /// <summary>
    /// Update an existing contact
    /// </summary>
    /// <param name="contact"></param>
    
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult UpdateContact(Contact contact)
    {
        if (contact is null)
            return BadRequest("Contact cannot be null.");

        var existingContact = _context.Contacts
            .AsNoTracking().FirstOrDefault(c=>c.Id == contact.Id);
        if (existingContact is null)
            return NotFound($"Contact with ID {contact.Id} was not found.");

        _context.Update(contact); 
        _context.SaveChanges();

        return Ok(existingContact);
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an existing contact by id
    /// </summary>
    /// <param name="id"></param>
    
    [HttpDelete("{id}")]
    public IActionResult DeleteContactById(int id)
    {
        var contact =  _context.Contacts
                        .AsNoTracking()
                        .FirstOrDefault(c=>c.Id == id);
        if (contact is null)
            return NotFound($"Contact with ID {id} was not found.");

        _context.Contacts.Remove(contact);
        _context.SaveChanges();

        return NoContent();
    }

    #endregion
}
