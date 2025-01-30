using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.Entities.TokenME
{
    [Table("TokenME", Schema = "UVA")]
    public class TokenME
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Result { get; set; }
    }
}
