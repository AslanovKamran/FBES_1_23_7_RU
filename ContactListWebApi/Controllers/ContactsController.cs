using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ContactListWebApi.Models;
using ContactListWebApi.Data;
using ContactListWebApi.Repository;
using System.Threading.Tasks;

namespace ContactListWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{

    private readonly IContactsRepository _repos;
    public ContactsController(IContactsRepository repos) => _repos = repos;

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
        var contacts = await _repos.GetContactsAsync();
        return Ok(contacts);
    }

    /// <summary>
    /// Get an existing contact by id   
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactById(int id)
    {
        var contact = await _repos.GetContactByIdAsync(id);

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
    public async Task<IActionResult> CreateContact(Contact contact)
    {
        if (contact is null)
            return BadRequest("Contact cannot be null.");

        var contactEntity = await _repos.CreateContactAsync(contact);

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
    public async Task<IActionResult> UpdateContact(Contact contact)
    {
        if (contact is null)
            return BadRequest("Contact cannot be null.");



        var updated = await _repos.UpdateContactAsync(contact);

        return Ok(updated);
    }

    #endregion

    #region Delete

    /// <summary>
    /// Delete an existing contact by id
    /// </summary>
    /// <param name="id"></param>

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContactById(int id)
    {
        await _repos.DeleteContactAsync(id);
        return NoContent();
    }

    #endregion
}
