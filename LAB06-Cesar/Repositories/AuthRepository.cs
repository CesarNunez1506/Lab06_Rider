using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LAB06_Cesar.Models;
using LAB06_Cesar.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LAB06_Cesar.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Lab06DbContext _context;

        public AuthRepository(Lab06DbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Login(string username, string password)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.User == username);
            if (user == null)
                return null;

            var storedHash = Convert.FromBase64String(user.Password);
            var storedSalt = Convert.FromBase64String(user.PasswordSalt);

            if (!VerifyPasswordHash(password, storedHash, storedSalt))
                return null;

            return user;
        }

        public async Task<Usuario> Register(Usuario user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Password = Convert.ToBase64String(passwordHash);
            user.PasswordSalt = Convert.ToBase64String(passwordSalt);

            await _context.Usuarios.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _context.Usuarios.AnyAsync(x => x.User == username);
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
