using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DataAccessUsers
{
    public class UsersRepository : IUsersRepository
    {
        private ConfigurationData _connectionString;
        //private readonly WebAppDbContext _context;
        //private readonly IMapper _mapper;
        public UsersRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }
        public async Task<IEnumerable<UserResponseDTO>> GetUsersAsync(int page = 1, int pageSize = 10)
        {
            try
            {
                using var dbConnection = await GetOpenDbConnectionAsync();
                int offset = (page - 1) * pageSize; // Calculamos el desplazamiento

                string storedProcedure = "[UVA].[SP_GET_USERS]";

                var persons = (await dbConnection.QueryAsync<UserResponseDTO>(
                    storedProcedure,
                    new { Offset = offset, PageSize = pageSize }, // Enviamos los parámetros a la SP
                    commandType: CommandType.StoredProcedure
                )).ToList();

                if (!persons.Any())
                {
                    throw ExceptionHandler.NullHandleException("Customers have not been found in the database.");
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
                    throw ExceptionHandler.NullHandleException("The Customer have not been found in the database.");
                }

                return person;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<UserResponseDTO> GetUserByParametersAsync(int? userTypeId, string? userId, string? email)
        {
            try
            {
                var parameters = new
                {
                    IdentificationId = userTypeId,
                    IdentificationNumber = userId,
                    Email = email
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_GET_USER_BY_PARAMETERS]";

                var person = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    storedProcedure,
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                if (person == null)
                {
                    throw ExceptionHandler.NullHandleException("The customer was not found in the database.");
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
        public async Task<UserResponseDTO?> UpdateUserAsync(UserCreateDTO userDto)
        {
            try
            {
                int personId = userDto.PersonId;

                // 🔥 1️⃣ Obtener el usuario actual antes de modificar
                var existingUser = await GetUserByIdAsync(personId);
                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                // 🔥 2️⃣ Ignorar valores `"string"` o `null`, usando los valores actuales
                userDto.IdentificationNumber = IsInvalid(userDto.IdentificationNumber) ? existingUser.IdentificationNumber : userDto.IdentificationNumber;
                userDto.ClientName = IsInvalid(userDto.ClientName) ? existingUser.ClientName : userDto.ClientName;
                userDto.ClientLastName = IsInvalid(userDto.ClientLastName) ? existingUser.ClientLastName : userDto.ClientLastName;
                userDto.Email = IsInvalid(userDto.Email) ? existingUser.Email : userDto.Email;
                userDto.Phone = IsInvalid(userDto.Phone) ? existingUser.Phone : userDto.Phone;
                userDto.RolId = existingUser.RolId;

                // 'UserCreateDTO' a 'UserME'
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
                    UserPasswordHash = user.UserPasswordHash,
                    user.UpdatedAt,
                    user.LastLogin
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                // 🔥 4️⃣ Ejecutar el SP y obtener el usuario actualizado
                var updatedUser = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    "[UVA].[SP_UPDATE_USER]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return updatedUser;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }





        public async Task<int> DeleteUserAsync(int typeId, int personId)
        {
            var parameters = new { IdentificationId = typeId, IdentificationNumber = personId };

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

        public async Task<UserAuthDTO?> ValidateUserAsync(string userName, string password)
        {
            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName, DbType.String);

                // 🔥 Obtener usuario desde la base de datos
                var user = await dbConnection.QueryFirstOrDefaultAsync<UserME>(
                    "[UVA].[SP_VALIDATE_USER]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // 🔥 Si el usuario no existe
                if (user == null)
                    return null;

                // 🔥 Verificar la contraseña con BCrypt (fuera del SP)
                if (!BCrypt.Net.BCrypt.Verify(password, user.UserPasswordHash))
                    return null; // Contraseña incorrecta

                // 🔥 Mapear `UserME` a `UserAuthDTO`
                return new UserAuthDTO
                {
                    UserName = user.UserName,
                    RolId = user.RolId ?? 0,
                    StatusId = user.StatusId,
                    LastLogin = user.LastLogin
                };
            }
            catch (SqlException ex)
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


        private async Task<IDbConnection> GetOpenDbConnectionAsync()
        {
            var dbConnection = DBConnection(); // Método que obtiene la conexión

            try
            {
                await dbConnection.OpenAsync();
                return dbConnection;
            }
            catch (SqlException sqlEx)
            {
                throw ExceptionHandler.HandleException(sqlEx);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }

        }



        // Para validar cuando se ingrese caracteres vacios o "string"
        private bool IsInvalid(string? value)
        {
            return string.IsNullOrWhiteSpace(value) || value.Equals("string", StringComparison.OrdinalIgnoreCase);
        }
    }
}

