using ChatAppXpress.Data;
using ChatAppXpress.Models;
using ChatAppXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(User request)
        {
            using (var authenticationService = new AuthenticationService(_context))
            {
                var isSigned = await authenticationService.SignUp(request);
                if(isSigned == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(isSigned);
                }
            }
        }
    }
}
