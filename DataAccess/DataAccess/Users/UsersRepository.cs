using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.Context;
using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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


        public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync()
        {
            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_GET_USERS]";

                var persons = await dbConnection.QueryAsync<UserResponseDTO>(
                    storedProcedure,
                    commandType: CommandType.StoredProcedure
                );

                if (persons == null)
                {
                    throw new Exception("Client not found.");
                }

                return persons;
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
                    user.CreatedAt,
                    user.UpdatedAt,
                    user.LastLogin,
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

        public async Task<List<int>> BulkInsertUsersAsync(List<UserCreateDTO> users)
        {
            var createdIds = new List<int>();

            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                //  Filtrar datos duplicados por IdentificationNumber y Email antes de insertar
                var uniqueUsers = users
                    .GroupBy(p => new { p.IdentificationNumber, p.Email }) // Agrupar por identificación y correo
                    .Select(g => g.First()) // Tomar solo el primer registro por grupo
                    .ToList();

                foreach (var userDto in uniqueUsers)
                {
                    //  Crear una instancia de UserME y generar el hash con SetPassword()
                    var userEntity = new UserME
                    {
                        UserName = userDto.UserName,
                        RolId = userDto.RolId,
                        StatusId = userDto.StatusId,
                        CreatedAt = userDto.CreatedAt,
                        UpdatedAt = userDto.UpdatedAt,
                        LastLogin = userDto.LastLogin
                    };
                    userEntity.SetPassword(userDto.Password); // ✅ Generar hash aquí

                    var parameters = new
                    {
                        userDto.IdentificationId,
                        userDto.IdentificationNumber,
                        userDto.ClientName,
                        userDto.ClientLastName,
                        userDto.GenderId,
                        userDto.Age,
                        userDto.Birthday,
                        userDto.Email,
                        userDto.Phone,
                        userDto.RolId,
                        userDto.StatusId,
                        userDto.UserName,
                        UserPasswordHash = userEntity.UserPasswordHash, // ✅ Hash ya generado
                        userDto.CreatedAt,
                        userDto.UpdatedAt,
                        userDto.LastLogin
                    };

                    int newPersonId = await dbConnection.QuerySingleAsync<int>(
                        "[UVA].[SP_INSERT_USER]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    createdIds.Add(newPersonId);
                }

                return createdIds; // Retorna la lista de IDs creados
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }



        public async Task<UserResponseDTO> AddUserToExistingPersonAsync(UserCreateDTO userDto)
        {
            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_INSERT_USER_TO_EXISTING_PERSON]";

                var userWithPersonInfo = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    storedProcedure,
                    new
                    {
                        userDto.PersonId,
                        userDto.UserName,
                        UserPasswordHash = userDto.Password, // Guardar solo el hash, // Ya debe estar hasheada antes de enviarla
                        userDto.RolId,
                        userDto.StatusId,
                        userDto.CreatedAt,
                        userDto.UpdatedAt,
                        LastLogin = (DateTime?)null // O la fecha actual si lo prefieres
                    },
                    commandType: CommandType.StoredProcedure
                );

                if (userWithPersonInfo == null)
                {
                    throw new Exception("No se pudo insertar el usuario o la persona no existe.");
                }

                return userWithPersonInfo;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<int> UpdateUserAsync(UserCreateDTO userDto)
        {
            try
            {
                var user = MapPersonDTOToEntity(userDto);
                user.UpdatedAt = DateTime.UtcNow;

                var parameters = new
                {
                    user.PersonId,
                    user.IdentificationId,
                    user.IdentificationNumber,
                    user.ClientName,
                    user.ClientLastName,
                    user.GenderId,
                    user.Age,
                    user.Birthday,
                    user.Email,
                    user.Phone,
                    user.UserName,
                    user.RolId,
                    user.StatusId,
                    UserPasswordHash = user.UserPasswordHash, // Solo si la contraseña se ha cambiado
                    user.UpdatedAt,
                    user.LastLogin
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                // Ejecutar el procedimiento almacenado para actualizar el usuario
                int affectedRows = await dbConnection.ExecuteAsync(
                    "[UVA].[SP_UPDATE_USER]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return affectedRows;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<int> DeleteUserAsync(int personId)
        {
            var parameters = new { PersonId = personId };

            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    // Ejecutar el SP y obtener las filas afectadas
                    int affectedRows = await dbConnection.ExecuteScalarAsync<int>("[UVA].[SP_DELETE_USER]", parameters, commandType: CommandType.StoredProcedure);

                    return affectedRows; // Retorna el número de filas eliminadas
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        private UserME MapPersonDTOToEntity(UserCreateDTO userDto)
        {
            var user = new UserME
            {
                PersonId = userDto.PersonId,
                IdentificationId = userDto.IdentificationId,
                IdentificationNumber = userDto.IdentificationNumber,
                ClientName = userDto.ClientName,
                ClientLastName = userDto.ClientLastName,
                GenderId = userDto.GenderId,
                Age = CalculateAge(userDto.Birthday),
                Birthday = userDto.Birthday,
                Email = userDto.Email,
                Phone = userDto.Phone,
                RolId = userDto.RolId,
                StatusId = userDto.StatusId,
                UserName = userDto.UserName,
                CreatedAt = userDto.CreatedAt,
                UpdatedAt = DateTime.UtcNow,  // Actualizamos la fecha de modificación
                LastLogin = userDto.LastLogin,
            };

            // Si la contraseña se actualiza, la procesamos
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.SetPassword(userDto.Password);  // Hash de la nueva contraseña
            }

            return user;
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

