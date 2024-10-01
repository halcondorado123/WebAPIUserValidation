using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.TokenME
{
    [Table("TokenME", Schema = "UVA")]
    public class TokenME
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public string? result { get; set; }
    }
}
