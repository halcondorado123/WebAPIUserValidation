using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("GenreME", Schema = "UVA")]
    public class GenreME
    {
        [Key]
        public int GenderId{ get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? GenderType{ get; set; }
    }
}
