using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiUserValidation.Models.Entities
{

    [Table("ClientME", Schema = "UVA")]
    public class ClientME : PersonME
    {
        [JsonIgnore]
        [Column("RolId")] // Nombre exacto en SQL
        public int? RoleId { get; set; }
        public RoleME? Role { get; set; } // Propiedad de navegación

        [Column("UsuId")] // Nombre exacto en SQL
        public int? UserInfoId { get; set; }  // Para la relación con UserInfoME
        public UserInfoME? UserInfo { get; set; } // Propiedad de navegación
    }
}
