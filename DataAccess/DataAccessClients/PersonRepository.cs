using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.ApiModelME;
using ApiUserValidation.Models.Entities.UserAttributesME;
using Azure;
using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.DataAccessClients
{
    public class PersonRepository : IPersonRepository
    {
        private ConfigurationData _connectionString;

        public PersonRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public async Task<List<PersonDTO>> GetClientsAsync()
        {
            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    string storedProcedure = "[UVA].[SP_GET_CLIENTS]";

                    // Ejecuta el procedimiento almacenado con Dapper
                    var clients = await dbConnection.QueryAsync<PersonDTO>(
                        storedProcedure,
                        commandType: CommandType.StoredProcedure
                    );

                    return clients.ToList(); 
                }
            }
            catch (SqlException sqlEx)
            {
                // Aquí puedes agregar un mensaje más específico para errores de SQL
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                throw new Exception("An error occurred while accessing the database. Please try again later.");
            }
            catch (TimeoutException timeoutEx)
            {
                // Manejo específico para tiempos de espera
                Console.WriteLine($"Timeout Error: {timeoutEx.Message}");
                throw new Exception("The request timed out. Please try again later.");
            }
            catch (InvalidOperationException invalidOpEx)
            {
                // Si ocurre un error en la conexión o en la ejecución de la consulta
                Console.WriteLine($"Operation Error: {invalidOpEx.Message}");
                throw new Exception("An unexpected error occurred while processing the request.");
            }
            catch (Exception ex)
            {
                // Manejo general de cualquier otro tipo de error
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unknown error occurred. Please contact support.");
            }
        }


        public async Task CreateAsync(PersonDTO personDto)
        {
            try
            {
                // Mapea el DTO a la entidad PersonME (puedes usar AutoMapper o hacer la conversión manualmente)
                var person = new PersonME
                {
                    IdentificationId = personDto.IdentificationId,
                    IdentificationNumber = personDto.IdentificationNumber,
                    ClientName = personDto.ClientName,
                    ClientLastName = personDto.ClientLastName,
                    GenderId = personDto.GenderId,
                    Age = personDto.Age,
                    Birthday = personDto.Birthday,
                    Email = personDto.Email,  // Asegúrate de mapear todos los campos correctamente
                    Phone = personDto.Phone,
                    UserId = personDto.UserId
                };

                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    string storedProcedure = "[UVA].[SP_INSERT_PERSON]";

                    var parameters = new
                    {
                        IdentificationId = person.IdentificationId,
                        IdentificationNumber = person.IdentificationNumber,
                        ClientName = person.ClientName,
                        ClientLastName = person.ClientLastName,
                        GenderId = person.GenderId,
                        Age = person.Age,
                        Birthday = person.Birthday,
                        Email = person.Email,
                        Phone = person.Phone,
                        UserId = person.UserId
                    };

                    await dbConnection.ExecuteAsync(
                        storedProcedure,
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}"); // Agregar StackTrace
                throw new Exception($"An error occurred while creating the person. {ex.Message}");
            }
        }


        //public ClientME GetClientById(int id)
        //{
        //    ClientME client = new ClientME();

        //    try
        //    {
        //        using (SqlConnection dbConnection = DBConnection())
        //        {
        //            using (SqlCommand command = new SqlCommand("[UVA].[SP_GET_CLIENT_BY_ID]", dbConnection))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.CommandTimeout = 9999;
        //                command.Parameters.AddWithValue("@ClientId", id);

        //                dbConnection.Open();

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        client.ClientId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;
        //                        client.RolId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;
        //                        client.Role = new RoleME
        //                        {
        //                            RolID = reader.IsDBNull(reader.GetOrdinal("RolID")) ? 0 : reader.GetInt32(reader.GetOrdinal("RolID")),
        //                            RolType = reader.IsDBNull(reader.GetOrdinal("RolType")) ? string.Empty : reader.GetString(reader.GetOrdinal("RolType"))
        //                        };  // Cambiar a coma aquí

        //                        //client.IdentificationId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;
        //                        client.Identification = new IdentificationME
        //                        {
        //                            IdentificationId = reader.GetValue(reader.GetOrdinal("IdentificationId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("IdentificationId")) : 0,
        //                            IdentificationType = !reader.IsDBNull(reader.GetOrdinal("IdentificationType")) ? reader.GetString(reader.GetOrdinal("IdentificationType")) : string.Empty
        //                        };

        //                        client.IdentificationNumber = !reader.IsDBNull(reader.GetOrdinal("IdentificationNumber")) ? reader.GetString(reader.GetOrdinal("IdentificationNumber")) : string.Empty;

        //                        client.ClientName = !reader.IsDBNull(reader.GetOrdinal("ClientName")) ? reader.GetString(reader.GetOrdinal("ClientName")) : string.Empty;
        //                        client.ClientLastName = !reader.IsDBNull(reader.GetOrdinal("ClientLastName")) ? reader.GetString(reader.GetOrdinal("ClientLastName")) : string.Empty;

        //                        //client.GenreId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;
        //                        client.Gender = new GenderME
        //                        {
        //                            GenderId = reader.IsDBNull(reader.GetOrdinal("GenreId")) ? 0 : reader.GetInt32(reader.GetOrdinal("GenreId")),
        //                            GenderType = !reader.IsDBNull(reader.GetOrdinal("GenderType")) ? reader.GetString(reader.GetOrdinal("GenderType")) : string.Empty
        //                        };

        //                        //client.RelatId = reader.GetValue(reader.GetOrdinal("ClientId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("ClientId")) : 0;
        //                        client.Relation = new RelationShME
        //                        {
        //                            RelatId = reader.GetValue(reader.GetOrdinal("RelatId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RelatId")) : 0,
        //                            RelationType = !reader.IsDBNull(reader.GetOrdinal("RelationType")) ? reader.GetString(reader.GetOrdinal("RelationType")) : string.Empty // Asegúrate de que este nombre coincida con el de tu SP
        //                        };

        //                        client.Age = reader.GetValue(reader.GetOrdinal("Age")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("Age")) : 0;
        //                        client.Birthday = reader.GetValue(reader.GetOrdinal("Birthday")) != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Birthday")) : DateTime.MinValue;
        //                        client.UsuId = reader.GetValue(reader.GetOrdinal("UsuId")) != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("UsuId")) : 0;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        Console.WriteLine($"SQL Error: {sqlEx.Message}");
        //        foreach (SqlError error in sqlEx.Errors)
        //        {
        //            Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"General Error: {ex.Message}");
        //    }

        //    return client;
        //}


        //public ApiResponse CreateClient(ClientME client)
        //{
        //    ApiResponse response = new ApiResponse();
        //    int id = 0;

        //    // Inicializar la conexión a la base de datos
        //    using (SqlConnection dbConnection = DBConnection()) // Asegúrate de que este método devuelva una conexión válida
        //    {
        //        try
        //        {
        //            // Calcular la edad antes de enviar al procedimiento almacenado
        //            client.Age = client.CalculateAge();

        //            using (SqlCommand command = new SqlCommand("[UVA].[SP_CREATE_CLIENT]", dbConnection))
        //            {
        //                command.CommandType = System.Data.CommandType.StoredProcedure;
        //                command.CommandTimeout = 9999;

        //                dbConnection.Open();

        //                //// Agregar el parámetro faltante de ClientId para la actualización
        //                //command.Parameters.AddWithValue("@ClientId", client.ClientId);
        //                command.Parameters.AddWithValue("@UsuId", client.UsuId);
        //                command.Parameters.AddWithValue("@RoleId", client?.Role?.RolID ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@IdentificationId", client?.Identification?.IdentificationId ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@IdentificationNumber", client?.IdentificationNumber);
        //                command.Parameters.AddWithValue("@ClientName", client?.ClientName);
        //                command.Parameters.AddWithValue("@ClientLastName", client?.ClientLastName);
        //                command.Parameters.AddWithValue("@GenreId", client?.Gender?.GenderId ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@RelatId", client?.Relation?.RelatId ?? (object)DBNull.Value);
        //                command.Parameters.AddWithValue("@Age", client?.Age); // Usa la edad calculada
        //                command.Parameters.AddWithValue("@Birthday", client?.Birthday);

        //                // Ejecuta el comando
        //                using (SqlDataReader dr = command.ExecuteReader())
        //                {
        //                    if (dr.Read())
        //                    {
        //                        // Puedes retornar el ID del cliente si lo necesitas
        //                        id = Convert.ToInt32(dr["NewClientId"]);
        //                    }
        //                }

        //                // Establecer la respuesta como exitosa
        //                response.Status = 200;
        //                response.Message = "Cliente creado con éxito.";
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            // Manejo de errores SQL
        //            response.Status = 500; // Indicar error interno del servidor
        //            response.Message = $"Error SQL: {ex.Message}";
        //        }
        //        catch (Exception ex)
        //        {
        //            // Manejo de otros errores
        //            response.Status = 500; // Indicar error interno del servidor
        //            response.Message = $"Error: {ex.Message}";
        //        }
        //        finally
        //        {
        //            // Cerrar la conexión si está abierta
        //            if (dbConnection != null)
        //            {
        //                dbConnection.Close();
        //                dbConnection.Dispose();
        //            }
        //        }
        //    }

        //    return response;
        //}

        //public ApiResponse ModifyClient(ClientME client)
        //{

        //    ApiResponse response = new ApiResponse();
        //    SqlConnection? dbConnection = null;

        //    try
        //    {
        //        dbConnection = DBConnection();


        //        using (SqlCommand command = new SqlCommand("[UVA].[SP_UPDATE_CLIENT]", dbConnection))
        //        {
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.CommandTimeout = 9999;

        //            dbConnection.Open();

        //            client.Age = client.CalculateAge();
        //            command.Parameters.AddWithValue("@ClientId", client.ClientId);
        //            command.Parameters.AddWithValue("@RoleId", client?.Role?.RolID ?? (object)DBNull.Value);
        //            command.Parameters.AddWithValue("@Identification", client?.Identification?.IdentificationId ?? (object)DBNull.Value);
        //            command.Parameters.AddWithValue("@IdentificationNumber", client?.IdentificationNumber);
        //            command.Parameters.AddWithValue("@ClientName", client?.ClientName);
        //            command.Parameters.AddWithValue("@ClientLastName", client?.ClientLastName);
        //            command.Parameters.AddWithValue("@GenreId", client?.Gender?.GenderId ?? (object)DBNull.Value);
        //            command.Parameters.AddWithValue("@RelatId", client?.Relation?.RelatId ?? (object)DBNull.Value);
        //            command.Parameters.AddWithValue("@Age", client?.Age); // Usa la edad calculada
        //            command.Parameters.AddWithValue("@Birthday", client?.Birthday);
        //            command.Parameters.AddWithValue("@UsuId", client?.UsuId);

        //            command.ExecuteNonQuery();

        //            response.Status = 200;
        //            response.Message = "Cliente actualizado con éxito.";

        //        }
        //    }

        //    catch (SqlException ex)
        //    {
        //        // Manejo de errores SQL
        //        response.Status = 500; // Indicar error interno del servidor
        //        response.Message = $"Error SQL: {ex.Message}";
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de otros errores
        //        response.Status = 500; // Indicar error interno del servidor
        //        response.Message = $"Error: {ex.Message}";
        //    }
        //    finally
        //    {
        //        // Cerrar la conexión si está abierta
        //        if (dbConnection != null)
        //        {
        //            dbConnection.Close();
        //            dbConnection.Dispose();
        //        }
        //    }

        //    return response;

        //}

        //public ApiResponse DeleteClient(int id)
        //{
        //    ApiResponse response = new ApiResponse();

        //    SqlConnection dbConnection = null;

        //    try
        //    {
        //        dbConnection = DBConnection();
        //        using (SqlCommand command = new SqlCommand("[UVA].[SP_DELETE_CLIENT]", dbConnection))
        //        {
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            command.CommandTimeout = 9999;

        //            command.Parameters.AddWithValue("@ClientId", id);

        //            dbConnection.Open();

        //            // Ejecutar el procedimiento almacenado
        //            command.ExecuteNonQuery();

        //            response.Status = 200;
        //            response.Message = "Cliente eliminado con éxito.";
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        response.Status = 500;
        //        response.Message = $"Error SQL: {ex.Message}";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = 500;
        //        response.Message = $"Error: {ex.Message}";
        //    }
        //    finally
        //    {
        //        // Cerrar la conexión si está abierta
        //        if (dbConnection != null)
        //        {
        //            dbConnection.Close();
        //            dbConnection.Dispose();
        //        }
        //    }

        //    return response;
        //}
    }
}
