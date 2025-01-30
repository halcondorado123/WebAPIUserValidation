using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.DTOs.UserAttributesDTO
{
    public class StatusDTO
    {
        public int StatusId { get; set; }
        public string StatusType { get; set; }
    }
}
