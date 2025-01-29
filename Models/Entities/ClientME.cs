using ApiUserValidation.Models.Entities.UserAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("ClientME", Schema = "UVA")]
public class ClientME
{
    [Key]
    [Column("ClientId")] // Nombre exacto en SQL
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ClientId { get; set; }

    [JsonIgnore]
    [Column("RolId")] // Nombre exacto en SQL
    public int? RolId { get; set; }

    [ForeignKey("RolId")]
    public RoleME? Role { get; set; } // Propiedad de navegación

    [JsonIgnore]
    [Column("IdentificationId")] // Nombre exacto en SQL
    public int? IdentificationId { get; set; }

    // Cambiar el ForeignKey para que apunte a IdentificationId
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

    [JsonIgnore]
    [Column("RelatId")] // Nombre exacto en SQL
    public int? RelatId { get; set; }

    [ForeignKey("RelatId")]
    public RelationShME? Relation { get; set; }

    [Column("Age")] // Nombre exacto en SQL
    public int Age { get; set; }

    [Column("Birthday")] // Nombre exacto en SQL
    public DateTime Birthday { get; set; }

    // Método para calcular la edad
    public int CalculateAge()
    {
        DateTime now = DateTime.Now;
        int age = now.Year - Birthday.Year;

        // Ajuste si el cumpleaños aún no ha ocurrido este año
        if (now < Birthday.AddYears(age))
        {
            age--;
        }

        return age;
    }

    [Column("UsuId")] // Nombre exacto en SQL
    public int UsuId { get; set; }
}
