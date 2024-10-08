using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("RoleME", Schema = "UVA")]
    public class RoleME
    {
        [Key]
        [Column("RolID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolID { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("RolType", TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? RolType { get; set; }
    }
}
