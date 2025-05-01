using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ValtercraftWebServer.Data;
using ValtercraftWebServer.DTO;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.Services
{
    public class UsersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _hasher;

        public UsersService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _hasher = new PasswordHasher<User>();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Username == username);
        }

        public async Task<User?> UpdateUser(int id, UpdateUserDto dto)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null) 
                return null;

            if(!string.IsNullOrEmpty(dto.Username))
                user.Username = dto.Username;
            if(!string.IsNullOrEmpty(dto.Password))
                user.Password = _hasher.HashPassword(user, dto.Password); ;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
