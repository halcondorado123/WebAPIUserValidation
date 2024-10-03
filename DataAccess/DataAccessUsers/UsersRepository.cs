using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
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
                    SqlCommand command = new SqlCommand("GET_USERS", dbConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure,
                        CommandTimeout = 9999
                    };

                    dbConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserInfoME user = new UserInfoME
                            {
                                UsuId = reader.GetInt32(0),
                                UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                ClientId = reader.GetInt32(2),
                                Identification = new IdentificationME
                                {
                                    IdentificationId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    IdentificationType = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                                },
                                IdentificationNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                ClientName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                ClientLastName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                RolId = new RoleME
                                {
                                    RolID = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                    RolType = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                                },
                                GenreId = new GenreME
                                {
                                    GenderId = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                    GenderType = reader.IsDBNull(11) ? string.Empty : reader.GetString(11)
                                },
                                RelatId = new RelationShME
                                {
                                    RelationId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                    RelationType = reader.IsDBNull(13) ? string.Empty : reader.GetString(13)
                                },
                                Age = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                                Birthday = reader.IsDBNull(15) ? DateTime.MinValue : reader.GetDateTime(15),
                                UserPassword = reader.IsDBNull(18) ? string.Empty : reader.GetString(18)
                            };

                            users.Add(user);
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
                Console.WriteLine(ex.Message);
            }

            return users;
        }



        public UserInfoME GetUserById(int userId)
        {
            UserInfoME user = null; // Inicializamos como null

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("GET_USER_BY_ID", dbConnection); // Asegúrate de tener este procedimiento almacenado
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    // Agregar el parámetro de entrada
                    command.Parameters.AddWithValue("@UserId", userId);

                    dbConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Solo necesitamos leer una vez
                        {
                            user = new UserInfoME
                            {
                                UsuId = reader.GetInt32(0),
                                UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                ClientId = reader.GetInt32(2), // Ya está en ClientME
                                Identification = new IdentificationME
                                {
                                    IdentificationId = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    IdentificationType = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                                },
                                IdentificationNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                                ClientName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                ClientLastName = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                RolId = new RoleME
                                {
                                    RolID = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                    RolType = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                                },
                                GenreId = new GenreME
                                {
                                    GenderId = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                    GenderType = reader.IsDBNull(11) ? string.Empty : reader.GetString(11)
                                },
                                RelatId = new RelationShME
                                {
                                    RelationId = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                                    RelationType = reader.IsDBNull(13) ? string.Empty : reader.GetString(13)
                                },
                                Age = reader.IsDBNull(14) ? 0 : reader.GetInt32(14),
                                Birthday = reader.IsDBNull(15) ? DateTime.MinValue : reader.GetDateTime(15),
                                UserPassword = reader.IsDBNull(18) ? string.Empty : reader.GetString(18)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: puedes registrar o manejar el error según sea necesario
                Console.WriteLine(ex.Message);
            }

            return user; // Retornamos el usuario o null si no se encontró
        }


        public int CreateUser(UserInfoME user)
        {
            int id = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("CREATE_USER", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    // Abre la conexión a la base de datos
                    dbConnection.Open();

                    // Agregar parámetros de entrada, manejando nulos
                    command.Parameters.AddWithValue("@IDENTI_ID", user.Identification?.IdentificationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IDENTI_NUMBER", string.IsNullOrEmpty(user.IdentificationNumber) ? (object)DBNull.Value : user.IdentificationNumber);
                    command.Parameters.AddWithValue("@CLIENT_NAME", string.IsNullOrEmpty(user.ClientName) ? (object)DBNull.Value : user.ClientName);
                    command.Parameters.AddWithValue("@CLIENT_LAST_NAME", string.IsNullOrEmpty(user.ClientLastName) ? (object)DBNull.Value : user.ClientLastName);
                    command.Parameters.AddWithValue("@GENRE_ID", user.GenreId?.GenderId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RELAT_ID", user.RelatId?.RelationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CLIENT_AGE", user.Age); // Si Age puede ser nulo, hazlo int?
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", user.Birthday == DateTime.MinValue ? (object)DBNull.Value : user.Birthday);
                    command.Parameters.AddWithValue("@USERNAME", string.IsNullOrEmpty(user.UserName) ? (object)DBNull.Value : user.UserName);
                    command.Parameters.AddWithValue("@USERPASSWORD", string.IsNullOrEmpty(user.UserPassword) ? (object)DBNull.Value : user.UserPassword);

                    // Ejecutar el comando y obtener el ID del nuevo usuario
                    id = (int)command.ExecuteScalar();

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
                SqlConnection dbConnection = DBConnection();
                SqlCommand command = new SqlCommand("VALIDATE_USER", dbConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandTimeout = 9999;
                command.Parameters.AddWithValue("@USERNAME", user.UserName);
                command.Parameters.AddWithValue("@USERPASSWORD", user.UserPassword);

                dbConnection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UsuId = (reader.GetValue(0) != DBNull.Value ? reader.GetInt32(0) : 0);
                        user.UserName = (!reader.IsDBNull(1) ? reader.GetString(1) : string.Empty);
                    }
                }
                dbConnection.Close();
                dbConnection.Dispose();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return user;
        }
        public bool UpdateUser(UserInfoME user)
        {
            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("UPDATE_USER", dbConnection);
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
                    command.Parameters.AddWithValue("@GENRE_ID", user.GenreId?.GenderId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RELAT_ID", user.RelatId?.RelationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CLIENT_AGE", user.Age); // Si Age puede ser nulo, hazlo int?
                    command.Parameters.AddWithValue("@CLIENT_BIRTHDAY", user.Birthday == DateTime.MinValue ? (object)DBNull.Value : user.Birthday);
                    command.Parameters.AddWithValue("@USERNAME", string.IsNullOrEmpty(user.UserName) ? (object)DBNull.Value : user.UserName);
                    command.Parameters.AddWithValue("@USERPASSWORD", string.IsNullOrEmpty(user.UserPassword) ? (object)DBNull.Value : user.UserPassword);

                    // Ejecutar el comando
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0; // Retorna true si se actualizó correctamente
                }
            }
            catch (Exception ex)
            {
                // Manejo del error: puedes registrar el error según sea necesario
                Console.WriteLine(ex.Message);
                return false; // Retorna false en caso de error
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("DELETE_USER", dbConnection);
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

