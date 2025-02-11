using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DataAccessUsers
{
    public class UsersRepository : IUsersRepository
    {
        private ConfigurationData _connectionString;

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
                user.SetPassword(userDto.Password);

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
                    UserPasswordHash = user.UserPasswordHash
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
            await using var dbConnection = DBConnection();
            await dbConnection.OpenAsync();

            using var transaction = await dbConnection.BeginTransactionAsync(); // Iniciar transacción

            try
            {
                var createdIds = new List<int>();

                var uniqueUsers = users
                    .GroupBy(p => new { p.IdentificationNumber, p.Email })
                    .Select(g => g.First());

                foreach (var userDto in uniqueUsers)
                {
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
                        UserPasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                    };

                    var userId = await dbConnection.QuerySingleAsync<int>(
                        "[UVA].[SP_INSERT_USER]",
                        parameters,
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure
                    );

                    createdIds.Add(userId);
                }

                await transaction.CommitAsync();

                return createdIds;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ExceptionHandler.HandleException(ex);
            }
        }



        public async Task<UserResponseDTO> AddUserToExistingPersonAsync(UserExistentDTO userDto)
        {

            if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ArgumentException("The UserPassword cannot be null or empty.");
            else if (userDto.Password.Contains(" ") || userDto.UserName.Contains(" "))
                throw new ArgumentException("The UserName or Password cannot contain spaces.");
            else if (userDto.UserName == "string" || userDto.Password == "string")
                throw new ArgumentException("Invalid values: UserName or Password cannot be 'string'.");

            var user = MapOnlyUserDTOToEntity(userDto);

            user.SetPassword(userDto.Password);

            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_INSERT_USER_TO_EXISTING_PERSON]";

                var userWithPersonInfo = await dbConnection.QueryFirstOrDefaultAsync<UserResponseDTO>(
                    storedProcedure,
                    new
                    {
                        userDto.IdentificationId,
                        userDto.IdentificationNumber,
                        userDto.UserName,
                        UserPasswordHash = user.UserPasswordHash,
                        userDto.RolId,
                        userDto.StatusId,
                    },

                    commandType: CommandType.StoredProcedure
                );

                if (userWithPersonInfo == null)
                    throw new Exception("The user could not be inserted or the person does not exist.");

                return userWithPersonInfo;
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<UserResponseDTO?> UpdateUserAsync(UserUpdateDTO userDto)
        {
                        if (string.IsNullOrWhiteSpace(userDto.Password))
                throw new ArgumentException("The UserPassword cannot be null or empty.");
            else if (userDto.Password.Contains(" ") || userDto.UserName.Contains(" "))
                throw new ArgumentException("The UserName or Password cannot contain spaces.");
            else if (userDto.UserName == "string" || userDto.Password == "string")
                throw new ArgumentException("Invalid values: UserName or Password cannot be 'string'.");

            try
            {
                int? userId = userDto.IdentificationId;
                string? identificationNumber = userDto.IdentificationNumber;
                string? email = userDto.Email;


                var existingUser = await GetUserByParametersAsync(userId, identificationNumber, email);
                if (existingUser == null)
                {
                    throw new Exception("User not found.");
                }

                userDto.IdentificationNumber = IsInvalid(userDto.IdentificationNumber) ? existingUser.IdentificationNumber : userDto.IdentificationNumber;
                userDto.ClientName = IsInvalid(userDto.ClientName) ? existingUser.ClientName : userDto.ClientName;
                userDto.ClientLastName = IsInvalid(userDto.ClientLastName) ? existingUser.ClientLastName : userDto.ClientLastName;
                userDto.Email = IsInvalid(userDto.Email) ? existingUser.Email : userDto.Email;
                userDto.Phone = IsInvalid(userDto.Phone) ? existingUser.Phone : userDto.Phone;
                userDto.RolId = existingUser.RolId;

                var user = MapPersonUpdateDTOToEntity(userDto);
                user.UpdatedAt = DateTime.UtcNow;

                var parameters = new
                {
                    //user.PersonId,
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
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

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

        public async Task<int?> DeleteUserAsync(int typeId, string identificationNumber)
        {
            var parameters = new { IdentificationId = typeId, IdentificationNumber = identificationNumber };

            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    int? deletedPersonId = await dbConnection.QuerySingleOrDefaultAsync<int?>(
                        "[UVA].[SP_DELETE_USER]",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    return deletedPersonId; // Retorna el ID eliminado
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

                // Obtener usuario desde la base de datos
                var user = await dbConnection.QueryFirstOrDefaultAsync<UserME>(
                    "[UVA].[SP_VALIDATE_USER]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                // Si el usuario no existe
                if (user == null)
                    return null;

                // Verificar la contraseña con BCrypt (fuera del SP)
                if (!BCrypt.Net.BCrypt.Verify(password, user.UserPasswordHash))
                    return null; // Contraseña incorrecta

                // Mapear `UserME` a `UserAuthDTO`
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
            };

            // Si la contraseña se actualiza, la procesamos
            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.SetPassword(userDto.Password);  // Hash de la nueva contraseña
            }

            return user;
        }

        private UserME MapOnlyUserDTOToEntity(UserExistentDTO userDto)
        {
            var user = new UserME
            {
                IdentificationId = userDto.IdentificationId,
                IdentificationNumber = userDto.IdentificationNumber,
                UserName = userDto.UserName,
                RolId = userDto.RolId,
                StatusId = userDto.StatusId,
            };


            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.SetPassword(userDto.Password);
            }

            return user;
        }

        private UserME MapPersonUpdateDTOToEntity(UserUpdateDTO userDto)
        {
            var user = new UserME
            {
                //PersonId = userDto.PersonId,
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

