using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using ApiUserValidation.Models.Entities.UserAttributesME;

namespace ApiUserValidation.Models.Entities
{
    [Table("PersonME", Schema = "UVA")]
    public class PersonME
    {
        [Key]
        [Column("ClientId")] // Nombre exacto en SQL
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [JsonIgnore]
        [Column("IdentificationId")] // Nombre exacto en SQL
        public int? IdentificationId { get; set; }

        [ForeignKey("IdentificationId")]
        public IdentificationME? Identification { get; set; }

        [Column("IdentificationNumber")] // Nombre exacto en SQL
        public string? IdentificationNumber { get; set; }

        [Column("ClientName")] // Nombre exacto en SQL
        public string? ClientName { get; set; }

        [Column("ClientLastName")] // Nombre exacto en SQL
        public string? ClientLastName { get; set; }

        [JsonIgnore]
        [Column("Gender")] // Nombre exacto en SQL
        public int? GenderId { get; set; }

        [ForeignKey("GenderId")]
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

        [Column("UsuId")] // Nombre exacto en SQL
        public int UsuId { get; set; }
        public UserInfoME? UserInfo { get; set; }  // Relación de uno a uno
    }
}