using ChatAppXpress.Data;
using ChatAppXpress.DTO;
using ChatAppXpress.Models;
using ChatAppXpress.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppXpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationRepository;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationRepository = authenticationService;
        }
        
        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp(User request)
        {
            using (authenticationRepository)
            {
                var isSigned = await authenticationRepository.SignUp(request);
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

        [HttpPost("/login")]
        public async Task<IActionResult> LogIng(UserDTO request){
            using(authenticationRepository){
                var requestJWT = await authenticationRepository.Authenticate(request);
                if( requestJWT == null){
                    return BadRequest();
                }
                return Ok(requestJWT);
            }
        }
    }
}
