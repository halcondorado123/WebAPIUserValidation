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
    public class UserInfoME
    {
        [Key]
        [Required]
        [MaxLength(100)]
        [Column("UserName", TypeName = "varchar(100)")] // Combina los dos atributos en uno
        public string? UserName { get; set; }

        // Almacena el hash de la contraseña
        [Required]
        [Column("UserPassword", TypeName = "varchar(200)")] // Combina aquí también
        public string? UserPassword { get; set; }
    }

}
