using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiUserValidation.Models.DTOs
{
    public class UserCreateDTO
    {
        public int IdentificationId { get; set; }
        public string IdentificationNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? GenderId { get; set; }
        public DateTime Birthday { get; set; }
        public int? RoleId { get; set; }
        public int StatusId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }  // Aquí se recibe la contraseña en texto plano

    }
}
