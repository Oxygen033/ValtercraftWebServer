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
        private readonly RconService _rconService;

        public WhiteListRequestService(ApplicationDbContext context, RconService rconService)
        {
            _context = context;
            _rconService = rconService;
        }

        public async Task<WhiteListRequestDto?> CreateWhiteListRequest(int userId, CreateWhiteListRequestDto dto)
        {
            List<WhiteListRequestDto> existingRequests = await GetAllWhiteListRequests(userId);
            foreach (WhiteListRequestDto request in existingRequests)
            {
                if (request.Status == WhiteListRequestStatus.PENDING.ToString() || request.Status == WhiteListRequestStatus.APPROVED.ToString())
                    return null;
            }

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
                Status = WhiteListRequestStatus.PENDING.ToString(),
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

        public async Task<List<WhiteListRequestDto>> GetAllWhiteListRequests(int? userId)
        {
            return await _context.WhiteListRequests
                .Include(req => req.User)
                .Select(req => new WhiteListRequestDto
                {
                    Id = req.Id,
                    Nickname = req.Nickname,
                    Reason = req.Reason,
                    Status = req.Status.ToString(),
                    User = new UserDto
                    {
                        Id = req.User.Id,
                        Username = req.User.Username,
                        Role = req.User.Role.ToString()
                    }
                }).Where(req => userId == null || req.User.Id == userId).ToListAsync();
        }


        public async Task<bool> SetRequestStatus(int requestId, int status) //0 - pending 1 - accepted 2 - declined
        {
            var request = await _context.WhiteListRequests
                .FirstOrDefaultAsync(r => r.Id == requestId);
            if (request == null)
                return false;
            request.Status = (WhiteListRequestStatus)status;
            await _context.SaveChangesAsync();

            if (status == 1)
                await _rconService.AddToWhiteList(request.Nickname);

            return true;
        }

        public async Task<bool> DeleteWhiteListRequest(int id)
        {
            WhiteListRequest? request = await _context.WhiteListRequests.FindAsync(id);
            if (request == null)
                return false;

            _context.WhiteListRequests.Remove(request);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
