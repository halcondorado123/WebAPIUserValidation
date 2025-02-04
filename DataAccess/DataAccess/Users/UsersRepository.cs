using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.Context;
using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessUsers
{
    public class UsersRepository : IUsersRepository
    {
        private ConfigurationData _connectionString;
        private readonly WebAppDbContext _context;
        private readonly IMapper _mapper;
        public UsersRepository(ConfigurationData connectionString, WebAppDbContext context, IMapper mapper)
        {
            _connectionString = connectionString;
            _context = context;
            _mapper = mapper;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<UserResponseDTO> GetUserByIdAsync(int personId)
        {
            try
            {
                var parameters = new { PersonId = personId };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_GET_USER_BY_ID]";

                var person = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (person == null)
                {
                    throw new Exception("Client not found.");
                }

                return person;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        //public async Task<UserResponseDTO> GetUserByIdAsync(int id)
        //{
        //    // Busca el usuario por su ID
        //    var user = await _context.Users
        //        .Where(u => u.UserId == id)
        //        .FirstOrDefaultAsync();

        //    // Si el usuario no existe, retorna null
        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    // Mapea el usuario encontrado a UserResponseDTO y lo retorna
        //    return _mapper.Map<UserResponseDTO>(user);
        //}

        public async Task<UserResponseDTO> CreateUserAsync(UserCreateDTO userDto)
        {
            var user = new UserME
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Phone = userDto.Phone,
                IdentificationNumber = userDto.IdentificationNumber,
                ClientName = userDto.ClientName,
                ClientLastName = userDto.ClientLastName,
                Birthday = userDto.Birthday,
                RoleId = userDto.RoleId,
                StatusId = userDto.StatusId
            };

            user.SetPassword(userDto.Password); // Usamos el método SetPassword para asignar el hash de la contraseña

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponseDTO>(user);
        }
    }
}

