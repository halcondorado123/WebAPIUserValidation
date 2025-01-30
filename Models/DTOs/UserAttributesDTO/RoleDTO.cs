using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs.UserAttributesDTO
{
    public class RoleDTO
    {
        public int RolID { get; set; }
        public string? RolType { get; set; }
    }
}
