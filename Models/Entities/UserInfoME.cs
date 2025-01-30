using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiUserValidation.Models.Entities
{
    [Table("UserInfoME", Schema = "UVA")]
    public class UserInfoME
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("PersonId")]
        [ForeignKey("PersonId")]
        public int? PersonId { get; set; }

        public PersonME? Person { get; set; }  // Relación con la persona

        [Required]
        [Column("UserName", TypeName = "varchar(100)")]
        public string? UserName { get; set; }

        [Required]
        [Column("UserPasswordHash", TypeName = "varchar(200)")]
        public string? UserPasswordHash { get; private set; } // Solo almacena el hash

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("LastLogin")]
        public DateTime? LastLogin { get; set; }
    }
}
