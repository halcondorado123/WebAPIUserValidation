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
        List<UserInfoME> GetUsers();
        UserInfoME GetUserById(int id);
        int CreateUser(UserInfoME user);
        UserInfoME ValidateUser(UserInfoME usuario);
        bool UpdateUser(UserInfoME user);
        bool DeleteUser(int id); // Opcional, si necesitas eliminar usuarios
    }
}
