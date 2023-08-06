using System.Security.Claims;
using ChatAppXpress.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppXpress.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController: Controller{
        private readonly IContactsService contactService;
        public ContactsController(IContactsService _contactService)
        {
            this.contactService = _contactService;
        }

        [HttpGet("contacts")]
        [Authorize]
        public async Task<IActionResult> ContactList(){

            var authUserId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if( authUserId != null ){
                using(contactService){
                    try{
                        var contactList = await contactService.GetContactList(Int32.Parse(authUserId));
                        return Ok(contactList);
                    }
                    catch(Exception ex){
                        return StatusCode(500,ex.Message);
                    }
                }
            }
            return BadRequest();
        }
    }
}