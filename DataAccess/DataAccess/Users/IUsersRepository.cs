using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
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
        Task<UserResponseDTO> AddUserToExistingPersonAsync(UserCreateDTO userDto);
        Task<int> UpdateUserAsync(UserCreateDTO userDto);

        //UserME ValidateUser(UserME usuario);
        //bool UpdateUser(UserME user);
        //bool DeleteUser(int id); // Opcional, si necesitas eliminar usuarios
    }
}
