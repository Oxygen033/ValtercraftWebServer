using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValtercraftWebServer.Data;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.Services
{
    public class UsersService
    {
        private readonly ApplicationDbContext _context;

        public UsersService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Username == username);
        }
    }
}
