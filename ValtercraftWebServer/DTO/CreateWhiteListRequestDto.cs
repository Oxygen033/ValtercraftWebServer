using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.DTO
{
    public class CreateWhiteListRequestDto
    {
        public string Nickname { get; set; }
        public string Reason { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
