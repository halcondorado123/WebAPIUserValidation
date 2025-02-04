using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public int IdentificationId { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? ClientName { get; set; }
        public string? ClientLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? GenderId { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public int? RoleId { get; set; }
        public int StatusId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
