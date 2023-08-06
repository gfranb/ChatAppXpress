using ChatAppXpress.Services;
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

        [HttpGet]
        public async Task<IActionResult> ContactList(){
            using(contactService){
                try{
                    var contactList = await contactService.GetContactList();
                    return Ok(contactList);
                }
                catch(Exception ex){
                    return StatusCode(500,ex.Message);
                }
            }
        }
    }
}