using ChatAppXpress.DTO;
using ChatAppXpress.Models;

namespace ChatAppXpress.Services
{
    public interface IContactsService : IDisposable
    {
        Task<List<User>> GetContactList();
    }
}