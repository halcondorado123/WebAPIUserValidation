using ApiUserValidation.Models.DTOs.UserAttributesDTO;
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

        [JsonIgnore]
        public RoleME? Role { get; set; } // Propiedad de navegación

        // Deja la propiedad UserId solo en ClientME si tiene un propósito específico
        [Column("UserId")] // Nombre exacto en SQL
        public int? UserId { get; set; }  // Relación con la tabla UserInfoME

        [JsonIgnore]
        public UserInfoME? UserInfo { get; set; } // Propiedad de navegación
    }
}
