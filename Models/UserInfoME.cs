using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("UserInfo", Schema = "UVA")]
    public class UserInfoME : ClientME
    {
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? UserName { get; set; }
        
        // Almacena el hash de la contraseña
        [Required]
        [Column(TypeName = "varchar(200)")]
        public string? UserPassword { get; set; }
    }
}
