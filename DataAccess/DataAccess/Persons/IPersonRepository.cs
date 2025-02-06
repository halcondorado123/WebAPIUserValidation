using ApiUserValidation.Models.DTOs;

namespace ApiUserValidation.Data.DataAccess.Persons
{
    public interface IPersonRepository
    {
        Task<List<PersonDTO>> GetPeopleAsync();
        Task<PersonDTO> GetPersonByIdAsync(int personId);
        Task<int> CreatePersonAsync(PersonDTO personDto);
        Task<List<int>> BulkInsertPeopleAsync(List<PersonDTO> people);
        Task UpdatePersonAsync(PersonDTO personDto);
        Task<int> DeletePersonAsync(int personId);
    }
}
