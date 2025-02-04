using ApiUserValidation.Models.Entities.UserAttributesME;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace ApiUserValidation.Models.Entities
{
    [Table("UserME", Schema = "UVA")]
    public class UserME : PersonME
    {
        [Required]
        [Column("UserName", TypeName = "varchar(100)")]
        public string? UserName { get; set; }

        [Required]
        [Column("UserPasswordHash", TypeName = "varchar(200)")]
        public string? UserPasswordHash { get; private set; } // Solo almacena el hash

        public void SetPassword(string password)
        {
            UserPasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        [Column("RolId")] // Nombre exacto en SQL
        public int? RolId { get; set; }

        [JsonIgnore]
        public RoleME? Role { get; set; }

        [Column("StatusId")]
        public int StatusId { get; set; }

        [JsonIgnore]
        public StatusME Status { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("LastLogin")]
        public DateTime? LastLogin { get; set; }

        // No es necesario tener la propiedad Person aquí debido a la herencia
    }

}
