using ChatAppXpress.DTO;
using ChatAppXpress.Models;

namespace ChatAppXpress.Services
{
    public interface IAuthenticationService : IDisposable
    {
        Task<String> Authenticate(UserDTO request);
        Task<int?> SignUp(User request);
    }
}
