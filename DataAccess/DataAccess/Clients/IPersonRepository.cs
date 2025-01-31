using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.ApiModelME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Data.DataAccess.Clients
{
    public interface IPersonRepository
    {
        Task<List<PersonDTO>> GetPeopleAsync();
        Task<PersonDTO> GetPersonByIdAsync(int personId);
        Task<int> CreatePersonAsync(PersonDTO personDto);
        Task UpdatePersonAsync(PersonDTO personDto);
        Task<List<PersonDTO>> DeletePersonAsync(int personId);
    }
}
