using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiUserValidation.Data.DataAccess.Persons
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

        public async Task<List<PersonDTO>> GetPeopleAsync()
        {
            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    string storedProcedure = "[UVA].[SP_GET_PEOPLE]";

                    var clients = await dbConnection.QueryAsync<PersonDTO>(
                        storedProcedure,
                        commandType: CommandType.StoredProcedure
                    );

                    return clients.ToList() ?? new List<PersonDTO>();
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int personId)
        {
            try
            {
                var parameters = new { PersonId = personId };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                string storedProcedure = "[UVA].[SP_GET_PERSON_BY_ID]";

                var person = await dbConnection.QueryFirstOrDefaultAsync<PersonDTO>(
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

        public async Task<int> CreatePersonAsync(PersonDTO personDto)
        {
            try
            {
                var person = MapPersonDTOToEntity(personDto);

                var parameters = new
                {
                    person.IdentificationId,
                    person.IdentificationNumber,
                    person.ClientName,
                    person.ClientLastName,
                    person.GenderId,
                    person.Age,
                    person.Birthday,
                    person.Email,
                    person.Phone
                };

                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                // Ejecutar el procedimiento almacenado y recuperar el ID insertado
                int newPersonId = await dbConnection.QuerySingleAsync<int>("[UVA].[SP_INSERT_PERSON]",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                return newPersonId; // Devuelve el ID creado
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }


        public async Task<List<int>> BulkInsertPeopleAsync(List<PersonDTO> people)
        {
            var createdIds = new List<int>();

            try
            {
                await using var dbConnection = DBConnection();
                await dbConnection.OpenAsync();

                // 🔥 Filtrar datos duplicados por IdentificationNumber y Email antes de insertar
                var uniquePeople = people
                    .GroupBy(p => new { p.IdentificationNumber, p.Email }) // Agrupar por identificación y correo
                    .Select(g => g.First()) // Tomar solo el primer registro por grupo
                    .ToList();

                foreach (var person in uniquePeople)
                {
                    var parameters = new
                    {
                        person.IdentificationId,
                        person.IdentificationNumber,
                        person.ClientName,
                        person.ClientLastName,
                        person.GenderId,
                        person.Age,
                        person.Birthday,
                        person.Email,
                        person.Phone
                    };

                    int newPersonId = await dbConnection.QuerySingleAsync<int>(
                        "[UVA].[SP_INSERT_PERSON]",
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

        public async Task UpdatePersonAsync(PersonDTO personDto)
        {
            try
            {
                int personId = personDto.PersonId;
                var existingPerson = await GetPersonByIdAsync(personId);

                if (existingPerson == null)
                {
                    throw new Exception("Person not found.");
                }

                var person = MapPersonDTOToEntity(personDto);
                personId = existingPerson.PersonId;

                var parameters = new
                {
                    personId,
                    person.IdentificationId,
                    person.IdentificationNumber,
                    person.ClientName,
                    person.ClientLastName,
                    person.GenderId,
                    person.Age,
                    person.Birthday,
                    person.Email,
                    person.Phone
                };

                await ExecuteStoredProcedureAsync("[UVA].[SP_UPDATE_PERSON]", parameters);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        public async Task<int> DeletePersonAsync(int personId)
        {
            var parameters = new { PersonId = personId };

            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.OpenAsync();

                    // Ejecutar el SP y obtener las filas afectadas
                    int affectedRows = await dbConnection.ExecuteScalarAsync<int>("[UVA].[SP_DELETE_PERSON]", parameters, commandType: CommandType.StoredProcedure);

                    return affectedRows; // Retorna el número de filas eliminadas
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        private PersonME MapPersonDTOToEntity(PersonDTO personDto)
        {
            return new PersonME
            {
                IdentificationId = personDto.IdentificationId,
                IdentificationNumber = personDto.IdentificationNumber,
                ClientName = personDto.ClientName,
                ClientLastName = personDto.ClientLastName,
                GenderId = personDto.GenderId,
                Age = personDto.Age,
                Birthday = personDto.Birthday,
                Email = personDto.Email,
                Phone = personDto.Phone,
            };
        }

        // 🔹 Método genérico para ejecutar cualquier Stored Procedure
        private async Task ExecuteStoredProcedureAsync(string storedProcedure, object parameters)
        {
            try
            {
                using (var dbConnection = DBConnection())
                {
                    await dbConnection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }
    }
}
