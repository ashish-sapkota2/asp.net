using Contactly.Data;
using Contactly.Models;
using Contactly.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contactly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactlyDbContext dbContext;

        public ContactsController(ContactlyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await dbContext.contacts.ToListAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddRequestDto contact)
        {
            var requestDomain = new Contact
            {
                Id = Guid.NewGuid(),
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Favourite = contact.Favourite,

            };
            dbContext.contacts.Add(requestDomain);
            dbContext.SaveChanges();
            return Ok(requestDomain);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var result= await dbContext.contacts.FindAsync(id);
            if(result is not null)
            {
                dbContext.contacts.Remove(result);
                dbContext.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
