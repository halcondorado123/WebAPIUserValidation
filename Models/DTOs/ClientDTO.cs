using ApiUserValidation.Models.Entities.UserAttributesME;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiUserValidation.Models.DTOs
{
    public class ClientDTO : PersonDTO
    {
        public int? RoleId { get; set; }
        public int? UserInfoId { get; set; }  // Para la relación con UserInfoME
    }
}
