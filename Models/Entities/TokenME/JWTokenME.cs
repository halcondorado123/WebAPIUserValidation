using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.Entities.TokenME
{
    [Table("JWTokenME", Schema = "UVA")]
    public class JWTokenME
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Subject { get; set; }
    }
}
