using ChatAppXpress.DTO;
using ChatAppXpress.Models;

namespace ChatAppXpress.Services
{
    public interface IAuthenticationService : IDisposable
    {
        Task<string?> Authenticate(UserDTO request);
        Task<int?> SignUp(User request);
        string GenerateJWT(User user);
    }
}
