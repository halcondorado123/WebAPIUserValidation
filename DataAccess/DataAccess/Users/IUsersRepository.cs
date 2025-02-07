using ApiUserValidation.Models.DTOs;

namespace ApiUserValidation.Data.DataAccess.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserResponseDTO>> GetUsersAsync(int page = 1, int pageSize = 10);
        Task<UserResponseDTO> GetUserByIdAsync(int personId);
        Task<int> CreateUserAsync(UserCreateDTO userDto);
        Task<List<int>> BulkInsertUsersAsync(List<UserCreateDTO> users);
        Task<UserResponseDTO> AddUserToExistingPersonAsync(UserCreateDTO userDto);
        Task<UserResponseDTO?> UpdateUserAsync(UserCreateDTO person);
        Task<int> DeleteUserAsync(int personId);
        Task<UserAuthDTO?> ValidateUserAsync(string userName, string password);
    }
}
