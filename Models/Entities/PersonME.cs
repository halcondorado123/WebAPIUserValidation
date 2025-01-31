using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ApiUserValidation.Models.Entities.UserAttributesME;
using ApiUserValidation.Models.DTOs.UserAttributesDTO;

namespace ApiUserValidation.Models.Entities
{
    [Table("PersonME", Schema = "UVA")]
    public class PersonME
    {
        [Key]
        [Column("PersonId")] // Nombre exacto en SQL
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [Column("IdentificationId")] // Nombre exacto en SQL
        public int? IdentificationId { get; set; }

        [JsonIgnore]
        [ForeignKey("IdentificationId")]
        public IdentificationME? Identification { get; set; }

        [Column("IdentificationNumber")] // Nombre exacto en SQL
        public string? IdentificationNumber { get; set; }

        [Column("ClientName")] // Nombre exacto en SQL
        public string? ClientName { get; set; }

        [Column("ClientLastName")] // Nombre exacto en SQL
        public string? ClientLastName { get; set; }

        [Column("GenderId")] // Nombre exacto en SQL
        public int? GenderId { get; set; }

        [JsonIgnore]
        public GenderME? Gender { get; set; }

        [Column("Age")] // Nombre exacto en SQL
        public int Age { get; set; }

        [Column("Birthday")] // Nombre exacto en SQL
        public DateTime Birthday { get; set; }

        public int CalculateAge()
        {
            DateTime now = DateTime.Now;
            int age = now.Year - Birthday.Year;
            if (now < Birthday.AddYears(age)) age--; // Ajuste si el cumpleaños aún no ha ocurrido este año
            return age;
        }

        [Column("UserId")] // Nombre exacto en SQL
        public int? UserId { get; set; }

        [Column("Email")] // Nombre exacto en SQL
        public string Email { get; set; }

        [Column("Phone")] // Nombre exacto en SQL
        public string Phone { get; set; }

        [JsonIgnore] 
        public UserInfoME? UserInfo { get; set; }  // Relación de uno a uno
    }
}