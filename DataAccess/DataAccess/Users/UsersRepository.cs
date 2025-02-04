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
using System.ComponentModel.DataAnnotations.Schema;
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


        public async Task<UserResponseDTO> GetUsersAsync()
        {
            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_GET_USERS]";

                var person = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    storedProcedure,
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

        public async Task<int> CreateUserAsync(UserCreateDTO userDto)
        {
            try
            {
                var user = MapPersonDTOToEntity(userDto);
                user.SetPassword(userDto.Password); // Hash de la contraseña

                var parameters = new
                {
                    user.IdentificationId,
                    user.IdentificationNumber,
                    user.ClientName,
                    user.ClientLastName,
                    user.GenderId,
                    user.Age,
                    user.Birthday,
                    user.Email,
                    user.Phone,
                    user.RolId,
                    user.StatusId,
                    user.UserName,
                    UserPasswordHash = user.UserPasswordHash // Guardar solo el hash
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                // Ejecutar el procedimiento almacenado y recuperar el ID insertado
                int newPersonId = await dbConnection.QuerySingleAsync<int>(
                    "[UVA].[SP_INSERT_USER]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return newPersonId; // Devuelve el ID de la persona creada
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        private UserME MapPersonDTOToEntity(UserCreateDTO userDto)
        {
            return new UserME
            {
                IdentificationId = userDto.IdentificationId,
                IdentificationNumber = userDto.IdentificationNumber,
                ClientName = userDto.ClientName,
                ClientLastName = userDto.ClientLastName,
                GenderId = userDto.GenderId,
                Age = CalculateAge(userDto.Birthday), // Calculamos la edad aquí
                Birthday = userDto.Birthday,
                Email = userDto.Email,
                Phone = userDto.Phone,
                RolId = userDto.RolId,
                StatusId = userDto.StatusId,
                UserName = userDto.UserName,
            };
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

            // Ajustar si la fecha de cumpleaños aún no ha pasado en este año
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }


    }
}

