using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs
{
    public class UserExistentDTO
    {
        public int IdentificationId { get; set; }
        public string IdentificationNumber { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; }
        public int? RolId { get; set; }
        public int StatusId { get; set; }
    }
}
