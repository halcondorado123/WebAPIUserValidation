using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.Entities.UserAttributes
{
    [Table("RelationShME", Schema = "UVA")]
    public class RelationShME
    {
        [Key]
        [Column("RelatId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RelatId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("RelationType", TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string? RelationType { get; set; }
    }
}
