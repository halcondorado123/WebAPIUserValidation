using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUserValidation.Models.DTOs.UserAttributesDTO
{
    public class GenderDTO
    {
        public int GenderId { get; set; }
        public string? GenderType { get; set; }
    }
}
