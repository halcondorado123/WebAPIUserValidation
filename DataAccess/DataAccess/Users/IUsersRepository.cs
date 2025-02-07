using ApiUserValidation.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiUserValidation.Data.DataAccess.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserResponseDTO>> GetUsersAsync(int page = 1, int pageSize = 10);
        Task<UserResponseDTO> GetUserByIdAsync(int personId);
        Task<UserResponseDTO> GetUserByParametersAsync(int? userTypeId, string? userId, string? email);
        Task<int> CreateUserAsync(UserCreateDTO userDto);
        Task<List<int>> BulkInsertUsersAsync(List<UserCreateDTO> users);
        Task<UserResponseDTO> AddUserToExistingPersonAsync(UserCreateDTO userDto);
        Task<UserResponseDTO?> UpdateUserAsync(UserCreateDTO person);
        Task<int> DeleteUserAsync(int typeId, int personId);
        Task<UserAuthDTO?> ValidateUserAsync(string userName, string password);
    }
}
