using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiUserValidation.Models.DTOs
{
    public class UserInfoDTO
    {
        public int UserId { get; set; }
        public int? PersonId { get; set; }
        public string? UserName { get; set; }
        public string? UserPasswordHash { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
