using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Models.Entities.UserAttributesME
{
    [Table("StatusME", Schema = "UVA")]
    public class StatusME
    {
        [Key]
        [Column("StatusId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")] // Especifica el tipo de datos en la base de datos
        public string StatusType { get; set; }
    }
}
