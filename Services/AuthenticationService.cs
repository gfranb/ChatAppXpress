using System.Security.Claims;
using ChatAppXpress.Data;
using ChatAppXpress.DTO;
using ChatAppXpress.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ChatAppXpress.Services
{
    public class AuthenticationService : IAuthenticationService, IDisposable
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;
        private bool disposed = false;

        public AuthenticationService(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            context = dbContext;
            this.configuration = configuration;
        }
        public async Task<string?> Authenticate(UserDTO request)
        {
            User? user = await context.Users.FirstOrDefaultAsync(user => user.UserName == request.Username);
            if(user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)){
                return null;
            }
            return GenerateJWT(user);
        }
        public string GenerateJWT(User request){
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, request.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: cred,
                expires: DateTime.Now.AddDays(1)
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
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
    }
}
