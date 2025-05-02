using ValtercraftWebServer.Data;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.Services
{
    public class WhiteListRequestService
    {
        private readonly ApplicationDbContext _context;

        public WhiteListRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<WhiteListRequest?> CreateUser()
    }
}
