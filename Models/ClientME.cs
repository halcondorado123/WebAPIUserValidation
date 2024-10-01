using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Table("ClientME", Schema = "UVA")]
    public class ClientME
    {
        [Key]
        public int ClientId { get; set; }
        public RoleME? RolId { get; set; }
        public IdentificationME? Identification { get; set; }
        public string? IdentificationNumber{ get; set; }
        public string? ClientName { get; set; }
        public string? ClientLastName { get; set; }
        public GenreME? GenreId { get; set; }
        public RelationShME? RelatId { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public int UsuId { get; set; }
    }
}
