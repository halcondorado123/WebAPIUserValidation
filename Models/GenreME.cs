using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("GenreME", Schema = "UVA")]
    public class GenreME
    {
        [Key]
        [Column("[GenreId]")]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("GenderType", TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? GenderType{ get; set; }
    }
}
