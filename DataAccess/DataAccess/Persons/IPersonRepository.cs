using ApiUserValidation.Models.DTOs;

namespace ApiUserValidation.Data.DataAccess.Persons
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
