using ChatAppXpress.Data;
using ChatAppXpress.DTO;
using ChatAppXpress.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAppXpress.Services
{
    public class AuthenticationService : IAuthenticationService, IDisposable
    {
        private readonly ApplicationDbContext context;

        public AuthenticationService(ApplicationDbContext dbContext)
        {
            context = dbContext;
        }

        public Task<string> Authenticate(UserDTO request)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> SignUp(User request)
        {
            try
            {
                if (request != null && await context.Users.FirstOrDefaultAsync(user => user.Email == request.Email) == null && await context.Users.FirstOrDefaultAsync(user => user.UserName == request.UserName) == null)
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    request.Password = passwordHash;
                    await context.Users.AddAsync(request);
                    await context.SaveChangesAsync();
                    return request.Id;
                }
                else
                {
                    return null;
                }
            }catch(Exception ex)
            {
                return null;
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Liberar recursos administrados
                    context.Dispose();
                }

                // Liberar recursos no administrados
                // ...

                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}
