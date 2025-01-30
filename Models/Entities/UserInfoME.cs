using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiUserValidation.Models.Entities
{
    [Table("UserInfo", Schema = "UVA")]
    public class UserInfoME
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("PersonId")]
        public int? PersonId { get; set; }

        [ForeignKey("PersonId")]
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
