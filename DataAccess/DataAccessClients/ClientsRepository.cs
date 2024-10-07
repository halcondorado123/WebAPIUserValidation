using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessClients
{
    public class ClientsRepository : IClientsRepository
    {
        private ConfigurationData _connectionString;

        public ClientsRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public List<ClientME> GetClients()
        {
            List<ClientME> clients = new List<ClientME>();

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    using (SqlCommand command = new SqlCommand("[UVA].[SP_GET_CLIENTS]", dbConnection))
                    {
                        command.CommandTimeout = 9999;

                        dbConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientME client = new ClientME
                                {
                                    ClientId = reader.IsDBNull(reader.GetOrdinal("ClientId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ClientId")),

                                    Role = new RoleME
                                    {
                                        RolID = reader.IsDBNull(reader.GetOrdinal("RolID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RolID")),
                                        RolType = reader.IsDBNull(reader.GetOrdinal("RolType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RolType"))
                                    },  // Cambiar a coma aquí

                                    Identification = new IdentificationME
                                    {
                                        IdentificationId = reader.IsDBNull(reader.GetOrdinal("IdentificationId")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdentificationId")),
                                        IdentificationType = reader.IsDBNull(reader.GetOrdinal("IdentificationType")) ? string.Empty : reader.GetString(reader.GetOrdinal("IdentificationType"))
                                    },

                                    IdentificationNumber = reader.IsDBNull(reader.GetOrdinal("IdentificationNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("IdentificationNumber")),
                                    ClientName = reader.IsDBNull(reader.GetOrdinal("ClientName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientName")),
                                    ClientLastName = reader.IsDBNull(reader.GetOrdinal("ClientLastName")) ? string.Empty : reader.GetString(reader.GetOrdinal("ClientLastName")),

                                    Genre = new GenreME
                                    {
                                        GenreId = reader.IsDBNull(reader.GetOrdinal("GenreId")) ? 0 : reader.GetInt32(reader.GetOrdinal("GenreId")),
                                        GenderType = reader.IsDBNull(reader.GetOrdinal("GenderType")) ? string.Empty : reader.GetString(reader.GetOrdinal("GenderType"))
                                    },

                                    Relation = new RelationShME
                                    {
                                        RelatId = reader.IsDBNull(reader.GetOrdinal("RelatId")) ? 0 : reader.GetInt32(reader.GetOrdinal("RelatId")),
                                        RelationType = reader.IsDBNull(reader.GetOrdinal("RelationType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RelationType"))
                                    },

                                    Age = reader.IsDBNull(reader.GetOrdinal("Age")) ? 0 : reader.GetInt32(reader.GetOrdinal("Age")),
                                    Birthday = reader.IsDBNull(reader.GetOrdinal("Birthday")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("Birthday")),
                                    UsuId = reader.IsDBNull(reader.GetOrdinal("UsuId")) ? 0 : reader.GetInt32(reader.GetOrdinal("UsuId"))
                                };

                                clients.Add(client);
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
                Console.WriteLine($"General Error: {ex.Message}");
            }

            return clients;
        }





        public ClientME GetClientById(int id)
        {
            ClientME client = new ClientME();

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    using (SqlCommand command = new SqlCommand("[UVA].[SP_GET_CLIENT_BY_ID]", dbConnection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandTimeout = 9999;
                        command.Parameters.AddWithValue("@ClientId", id);

                        dbConnection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.ClientId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;

                                client.Role = new RoleME
                                {
                                    RolID = reader.IsDBNull(reader.GetOrdinal("RolID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RolID")),
                                    RolType = reader.IsDBNull(reader.GetOrdinal("RolType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RolType"))
                                };  // Cambiar a coma aquí

                                client.Identification = new IdentificationME
                                {
                                    IdentificationId = reader.GetValue(reader.GetOrdinal("IdentificationId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("IdentificationId")) : 0,
                                    IdentificationType = !reader.IsDBNull(reader.GetOrdinal("IdentificationType")) ? reader.GetString(reader.GetOrdinal("IdentificationType")) : string.Empty
                                };

                                client.IdentificationNumber = !reader.IsDBNull(reader.GetOrdinal("IdentificationNumber")) ? reader.GetString(reader.GetOrdinal("IdentificationNumber")) : string.Empty;

                                client.ClientName = !reader.IsDBNull(reader.GetOrdinal("ClientName")) ? reader.GetString(reader.GetOrdinal("ClientName")) : string.Empty;
                                client.ClientLastName = !reader.IsDBNull(reader.GetOrdinal("ClientLastName")) ? reader.GetString(reader.GetOrdinal("ClientLastName")) : string.Empty;

                                client.Genre = new GenreME
                                {
                                    GenreId = reader.IsDBNull(reader.GetOrdinal("GenreId")) ? 0 : reader.GetInt32(reader.GetOrdinal("GenreId")),
                                    GenderType = !reader.IsDBNull(reader.GetOrdinal("GenderType")) ? reader.GetString(reader.GetOrdinal("GenderType")) : string.Empty
                                };

                                client.Relation = new RelationShME
                                {
                                    RelatId = reader.GetValue(reader.GetOrdinal("RelatId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RelatId")) : 0,
                                    RelationType = !reader.IsDBNull(reader.GetOrdinal("RelationType")) ? reader.GetString(reader.GetOrdinal("RelationType")) : string.Empty // Asegúrate de que este nombre coincida con el de tu SP
                                };

                                client.Age = reader.GetValue(reader.GetOrdinal("Age")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("Age")) : 0;
                                client.Birthday = reader.GetValue(reader.GetOrdinal("Birthday")) != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Birthday")) : DateTime.MinValue;
                                client.UsuId = reader.GetValue(reader.GetOrdinal("UsuId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("UsuId")) : 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Manejo de excepciones
            }

            return client;
        }


        public int CreateClient(ClientME client)
        {
            int id = 0;

            try
            {
                // Calcular la edad antes de enviar al procedimiento almacenado
                client.Age = client.CalculateAge();

                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_CREATE_CLIENT]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@UsuId", client.UsuId);
                    command.Parameters.AddWithValue("@RoleId", client?.Role?.RolID ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdentificationId", client?.Identification?.IdentificationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdentificationNumber", client?.IdentificationNumber);
                    command.Parameters.AddWithValue("@ClientName", client?.ClientName);
                    command.Parameters.AddWithValue("@ClientLastName", client?.ClientLastName);
                    command.Parameters.AddWithValue("@GenreId", client?.Genre?.GenreId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RelatId", client?.Relation?.RelatId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Age", client?.Age); // Usa la edad calculada
                    command.Parameters.AddWithValue("@Birthday", client?.Birthday);

                    // Ejecuta el comando
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = Convert.ToInt32(dr["NewClientId"]);
                        }
                    }

                    dbConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return id;
        }





        public int ModifyClient(ClientME client)
        {

            int UpdateUser = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_UPDATE_CLIENT]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@UsuId", client.UsuId);
                    command.Parameters.AddWithValue("@Identification", client.Identification?.IdentificationId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IdentificationNumber", client.IdentificationNumber);
                    command.Parameters.AddWithValue("@ClientName", client.ClientName);
                    command.Parameters.AddWithValue("@ClientLastName", client.ClientLastName);
                    command.Parameters.AddWithValue("@GenreId", client.Genre?.GenreId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RelatId", client.Relation?.RelatId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Age", client.Age);
                    command.Parameters.AddWithValue("@Birthday", client.ClientId);
                    command.Parameters.AddWithValue("@ClientId", client.ClientId);


                    command.ExecuteNonQuery();

                    UpdateUser = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return UpdateUser;

        }


        public int DeleteClient(int id)
        {

            int ValidateDelete = 0;

            try
            {
                using (SqlConnection dbConnection = DBConnection())
                {
                    SqlCommand command = new SqlCommand("[UVA].[SP_DELETE_CLIENT]", dbConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandTimeout = 9999;

                    dbConnection.Open();

                    command.Parameters.AddWithValue("@ClientId", id);

                    command.ExecuteNonQuery();

                    ValidateDelete = 1;

                    dbConnection.Close();
                    dbConnection.Dispose();
                }
            }

            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return ValidateDelete;

        }

    }
}
