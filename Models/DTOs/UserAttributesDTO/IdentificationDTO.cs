using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs.UserAttributesDTO
{
    public class IdentificationDTO
    {
        public int IdentificationId { get; set; }
        public string? IdentificationType { get; set; }
    }
}
