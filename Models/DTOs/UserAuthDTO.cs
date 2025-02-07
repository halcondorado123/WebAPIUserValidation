using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs
{
    public class UserAuthDTO
    {
        public string UserName { get; set; }
        public int RolId { get; set; }
        public int StatusId { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
