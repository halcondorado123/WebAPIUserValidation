using Microsoft.Data.SqlClient;
using Models;
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
        public UsersRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }


        public List<UserInfoME> GetUsers()
        {
            List<UserInfoME> users = new List<UserInfoME>();

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    using (SqlCommand command = new SqlCommand("[UVA].[SP_GET_USERS]", dbConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 9999;

                        dbConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserInfoME user = new UserInfoME
                                {
                                    UsuId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0,
                                    UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserName")),
                                    ClientId = reader.GetInt32(reader.GetOrdinal("ClientId")),
                                    Identification = new IdentificationME
                                    {
                                        IdentificationId = reader.IsDBNull(reader.GetOrdinal("IdentificationId")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdentificationId")),
                                        IdentificationType = reader.IsDBNull(reader.GetOrdinal("IdentificationType")) ? string.Empty : reader.GetString(reader.GetOrdinal("IdentificationType"))
                                    },
                                    IdentificationNumber = reader.IsDBNull(reader.GetOrdinal("IdentificationNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("IdentificationNumber")),
                                    ClientName = reader.IsDBNull(reader.GetOrdinal("ClientName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientName")),
                                    ClientLastName = reader.IsDBNull(reader.GetOrdinal("ClientLastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientLastName")),
                                    Role = new RoleME
                                    {
                                        RolID = reader.IsDBNull(reader.GetOrdinal("RolID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RolID")), // Cambiar a int?
                                        RolType = reader.IsDBNull(reader.GetOrdinal("RolType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RolType"))
                                    },
                                    Genre = new GenreME
                                    {
                                        GenreId = reader.IsDBNull(reader.GetOrdinal("GenderId")) ? 0 : reader.GetInt32(reader.GetOrdinal("GenderId")), // Cambiar a int?
                                        GenderType = reader.IsDBNull(reader.GetOrdinal("GenderType")) ? string.Empty : reader.GetString(reader.GetOrdinal("GenderType"))
                                    },

                                    Relation = new RelationShME
                                    {
                                        RelatId = reader.IsDBNull(reader.GetOrdinal("RelatId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RelatId")), // Cambiar a int?
                                        RelationType = reader.IsDBNull(reader.GetOrdinal("RelationType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RelationType"))
                                    },

                                    Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? 0 : reader.GetInt32(reader.GetOrdinal("Age")),
                                    Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Birthday")),
                                    UserPassword = reader.IsDBNull(reader.GetOrdinal("UserPassword")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserPassword"))
                                };

                                users.Add(user);
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                foreach (SqlError error in sqlEx.Errors)
                {
                    Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message} at GenderId");
            }

            return users;
        }


        public UserInfoME GetUserById(int userId)
        {
            UserInfoME user = new UserInfoME();

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    using (SqlCommand command = new SqlCommand("[UVA].[SP_GET_USERS_BY_ID]", dbConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 9999;

                        // Agregar el parámetro de entrada
                        command.Parameters.AddWithValue("@UserId", userId);

                        dbConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user.UsuId = reader.GetValue(reader.GetOrdinal("UsuId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("UsuId")) : 0;

                                user.UserName = reader.IsDBNull(reader.GetOrdinal("UserName")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserName"));
                                user.ClientId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;

                                user.Identification = new IdentificationME
                                {
                                    IdentificationId = reader.GetValue(reader.GetOrdinal("IdentificationId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("IdentificationId")) : 0,
                                    IdentificationType = !reader.IsDBNull(reader.GetOrdinal("IdentificationType")) ? reader.GetString(reader.GetOrdinal("IdentificationType")) : string.Empty
                                };

                                user.IdentificationNumber = !reader.IsDBNull(reader.GetOrdinal("IdentificationNumber")) ? reader.GetString(reader.GetOrdinal("IdentificationNumber")) : string.Empty;

                                user.ClientName = !reader.IsDBNull(reader.GetOrdinal("ClientName")) ? reader.GetString(reader.GetOrdinal("ClientName")) : string.Empty;
                                user.ClientLastName = !reader.IsDBNull(reader.GetOrdinal("ClientLastName")) ? reader.GetString(reader.GetOrdinal("ClientLastName")) : string.Empty;

                                user.Role = new RoleME
                                {
                                    RolID = reader.IsDBNull(reader.GetOrdinal("RolID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RolID")), // Cambiar a int?
                                    RolType = reader.IsDBNull(reader.GetOrdinal("RolType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RolType"))
                                };

                                user.Genre = new GenreME
                                {
                                    GenreId = reader.IsDBNull(reader.GetOrdinal("GenderId")) ? 0 : reader.GetInt32(reader.GetOrdinal("GenderId")), // Cambiar a int?
                                    GenderType = reader.IsDBNull(reader.GetOrdinal("GenderType")) ? string.Empty : reader.GetString(reader.GetOrdinal("GenderType"))
                                };

                                user.Relation = new RelationShME
                                {
                                    RelatId = reader.IsDBNull(reader.GetOrdinal("RelatId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RelatId")), // Cambiar a int?
                                    RelationType = reader.IsDBNull(reader.GetOrdinal("RelationType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RelationType"))
                                }; 

                                user.Age = reader.GetValue(reader.GetOrdinal("Age")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("Age")) : 0;
                                user.Birthday = reader.GetValue(reader.GetOrdinal("Birthday")) != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Birthday")) : DateTime.MinValue;
                                user.UserPassword = reader.IsDBNull(reader.GetOrdinal("UserPassword")) ? string.Empty : reader.GetString(reader.GetOrdinal("UserPassword"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Manejo de excepciones
            }

            return user;
        }


        public int CreateUser(UserInfoME user)
        {
            int id = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_CREATE_USER]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    // Abre la conexión a la base de datos
                    dbConnection.Open();

                    // Agregar parámetros de entrada, manejando nulos
                    command.Parameters.AddWithValue("@IdentificationId", user.Identification?.IdentificationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdentificationNumber", string.IsNullOrEmpty(user.IdentificationNumber) ? (object)DBNull.Value : user.IdentificationNumber);
                    command.Parameters.AddWithValue("@ClientName", string.IsNullOrEmpty(user.ClientName) ? (object)DBNull.Value : user.ClientName);
                    command.Parameters.AddWithValue("@ClientLastName", string.IsNullOrEmpty(user.ClientLastName) ? (object)DBNull.Value : user.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", user.Genre?.GenreId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RELAT_ID", user.Relation?.RelatId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Age", user.Age);
                    command.Parameters.AddWithValue("@Birthday", user.Birthday == DateTime.MinValue ? (object)DBNull.Value : user.Birthday);
                    command.Parameters.AddWithValue("@UserName", string.IsNullOrEmpty(user.UserName) ? (object)DBNull.Value : user.UserName);
                    command.Parameters.AddWithValue("@UserPassword", string.IsNullOrEmpty(user.UserPassword) ? (object)DBNull.Value : user.UserPassword);

                    // Ejecutar el comando y obtener el ID del nuevo usuario
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = dr.GetInt32(0); // Suponiendo que el ID es devuelto como el primer valor
                        }
                    }

                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: puedes registrar el error según sea necesario
                Console.WriteLine(ex.Message);
            }

            return id; // Retorna el ID del nuevo usuario
        }

        public UserInfoME ValidateUser(UserInfoME user)
        {
            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    using (SqlCommand command = new SqlCommand("[UVA].[SP_VALIDATE_USER]", dbConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 9999;

                        // Agregar parámetros
                        command.Parameters.AddWithValue("@UserName", user.UserName);
                        command.Parameters.AddWithValue("@UserPassword", user.UserPassword);

                        dbConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Usar un solo read para obtener los datos del usuario
                            {
                                user.UsuId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                user.UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: considera registrar el error
                Console.WriteLine(ex.Message);
            }

            return user;
        }

        public bool UpdateUser(UserInfoME user)
        {
            bool isUpdated = false;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_UPDATE_USER]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    // Abre la conexión a la base de datos
                    dbConnection.Open();

                    // Agregar parámetros, manejando nulos
                    command.Parameters.AddWithValue("@USU_ID", user.UsuId); // Supongamos que UsuId es necesario
                    command.Parameters.AddWithValue("@IDENTI_ID", user.Identification?.IdentificationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", string.IsNullOrEmpty(user.IdentificationNumber) ? (object)DBNull.Value : user.IdentificationNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", string.IsNullOrEmpty(user.ClientName) ? (object)DBNull.Value : user.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", string.IsNullOrEmpty(user.ClientLastName) ? (object)DBNull.Value : user.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", user.Genre?.GenreId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RELAT_ID", user.Relation?.RelatId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CLIENT_AGE", user.Age); // Si Age puede ser nulo, hazlo int?
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", user.Birthday == DateTime.MinValue ? (object)DBNull.Value : user.Birthday);
                    command.Parameters.AddWithValue("@USERNAME", string.IsNullOrEmpty(user.UserName) ? (object)DBNull.Value : user.UserName);
                    command.Parameters.AddWithValue("@USERPASSWORD", string.IsNullOrEmpty(user.UserPassword) ? (object)DBNull.Value : user.UserPassword);

                    // Ejecutar el comando
                    int rowsAffected = command.ExecuteNonQuery();

                    isUpdated = rowsAffected > 0; // Retorna true si se actualizó correctamente
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: puedes registrar el error según sea necesario
                Console.WriteLine(ex.Message);
            }

            return isUpdated; // Retorna el resultado de la actualización
        }


        public bool DeleteUser(int id)
        {
            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_DELETE_USER]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;
                    command.Parameters.AddWithValue("@USU_ID", id);

                    dbConnection.Open();
                    int rowsAffected = command.ExecuteNonQuery(); // Ejecuta el comando

                    return rowsAffected > 0; // Retorna verdadero si se eliminó al menos un registro
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Manejo de errores, puedes registrar el error como necesites
                return false; // Retorna falso en caso de error
            }
        }
    }
}

