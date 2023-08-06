using ChatAppXpress.Data;
using ChatAppXpress.Models;

namespace ChatAppXpress.Services{
    public class ContactsService : IContactsService
    {
        private readonly ApplicationDbContext context;

        public ContactsService(ApplicationDbContext context)
        {
            this.context = context;            
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetContactList(int requestId)
        {
            List<User> contacts = context.Users.Where(user => user.Id == requestId).SelectMany(u => u.Contacts).ToList();
            return Task.FromResult(contacts);
        }
    }
}