using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs
{
    public class UserUpdateDTO
    {
        //public int PersonId { get; set; }
        public int? IdentificationId { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? ClientName { get; set; }
        public string? ClientLastName { get; set; }
        public int? GenderId { get; set; }
        public int Age => DateTime.Now.Year - Birthday.Year - (DateTime.Now < Birthday.AddYears(DateTime.Now.Year - Birthday.Year) ? 1 : 0);
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RolId { get; set; }
        public int StatusId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}


