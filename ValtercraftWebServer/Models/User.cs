using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ValtercraftWebServer.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.USER;

        [Required]
        public ICollection<WhiteListRequest> WhiteListRequests { get; set; }
    }

    public enum UserRole
    {
        ADMIN,
        USER
    }
}
