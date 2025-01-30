using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUserValidation.Models.Entities.UserAttributesME
{
    [Table("GenderME", Schema = "UVA")]
    public class GenderME
    {
        [Key]
        [Column("GenderId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenderId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("GenderType", TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? GenderType { get; set; }
    }
}
