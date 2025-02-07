using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUserValidation.Data.DataAccess.Users
{
    public interface IUsersRepository
    {
        Task<IEnumerable<UserResponseDTO>> GetUsersAsync();
        Task<UserResponseDTO> GetUserByIdAsync(int personId);
        Task<int> CreateUserAsync(UserCreateDTO userDto);
        Task<List<int>> BulkInsertUsersAsync(List<UserCreateDTO> users);
        Task<UserResponseDTO> AddUserToExistingPersonAsync(UserCreateDTO userDto);
        Task<int> UpdateUserAsync(UserCreateDTO userDto);
        Task<int> DeleteUserAsync(int personId);
        Task<UserAuthDTO?> ValidateUserAsync(string userName, string password);
    }
}
