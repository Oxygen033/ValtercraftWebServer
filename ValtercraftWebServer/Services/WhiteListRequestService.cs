using Microsoft.EntityFrameworkCore;
using System.Drawing;
using ValtercraftWebServer.Data;
using ValtercraftWebServer.DTO;
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

        public async Task<WhiteListRequestDto?> CreateWhiteListRequest(int userId, CreateWhiteListRequestDto dto)
        {
            WhiteListRequest whiteListRequest = new WhiteListRequest()
            {
                Nickname = dto.Nickname,
                Reason = dto.Reason,
                UserId = userId,
            };
            await _context.WhiteListRequests.AddAsync(whiteListRequest);
            await _context.SaveChangesAsync();

            await _context.Entry(whiteListRequest).Reference(r => r.User).LoadAsync();

            return new WhiteListRequestDto
            {
                Id = whiteListRequest.Id,
                Nickname = whiteListRequest.Nickname,
                Reason = whiteListRequest.Reason,
                Status = whiteListRequest.Status.ToString(),
                User = new UserDto
                {
                    Id = whiteListRequest.User.Id,
                    Username = whiteListRequest.User.Username,
                    Role = whiteListRequest.User.Role.ToString()
                }
            };
        }

        public async Task<WhiteListRequestDto?> GetWhiteListRequest(int id)
        {
            var request = await _context.WhiteListRequests
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
                return null;

            return new WhiteListRequestDto
            {
                Id = request.Id,
                Nickname = request.Nickname,
                Reason = request.Reason,
                User = new UserDto
                {
                    Id = request.User.Id,
                    Username = request.User.Username,
                    Role = request.User.Role.ToString()
                }
            };
        }

    }
}
