using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Clients;
using ApiUserValidation.Data.DataAccess.DataService;
using ApiUserValidation.Models.DTOs;
using Dapper;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Xunit;

namespace ApiUserValidation.Testing
{
    public class PeopleTesting
    {
        private readonly PersonRepository _personRepository;

        public PeopleTesting()
        {
            // Crea el mock de ConfigurationData
            var mockConfigData = new Mock<ConfigurationData>();  // Para connectionString

            // Crea el mock de IDataService
            var mockDataService = new Mock<IDataService>();

            // Crea un mock del IDbConnection
            var mockDbConnection = new Mock<IDbConnection>();

            // Configura el mock para devolver datos cuando se llama a QueryAsync
            mockDbConnection.Setup(conn => conn.QueryAsync<PersonDTO>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
                             .ReturnsAsync(new List<PersonDTO>
                             {
                     new PersonDTO
                     {
                         PersonId = 1, IdentificationId = 1, IdentificationNumber = "A12345678", ClientName = "Juan",
                         ClientLastName = "Pérez", GenderId = 1, Birthday = new DateTime(1985, 4, 12), UserId = 1,
                         Email = "juan.perez@example.com", Phone = "123-456-7890"
                     },
                     new PersonDTO
                     {
                         PersonId = 2, IdentificationId = 2, IdentificationNumber = "B98765432", ClientName = "Ana",
                         ClientLastName = "Gómez", GenderId = 2, Birthday = new DateTime(1990, 6, 22), UserId = 2,
                         Email = "ana.gomez@example.com", Phone = "234-567-8901"
                     },
                     new PersonDTO
                     {
                         PersonId = 3, IdentificationId = 3, IdentificationNumber = "C11223344", ClientName = "Carlos",
                         ClientLastName = "Lopez", GenderId = 1, Birthday = new DateTime(1982, 11, 30), UserId = 3,
                         Email = "carlos.lopez@example.com", Phone = "345-678-9012"
                     },
                     new PersonDTO
                     {
                         PersonId = 4, IdentificationId = 4, IdentificationNumber = "D44332211", ClientName = "María",
                         ClientLastName = "Martínez", GenderId = 2, Birthday = new DateTime(1995, 3, 14), UserId = 4,
                         Email = "maria.martinez@example.com", Phone = "456-789-0123"
                     },
                     new PersonDTO
                     {
                         PersonId = 5, IdentificationId = 5, IdentificationNumber = "E55667788", ClientName = "Pedro",
                         ClientLastName = "Rodríguez", GenderId = 1, Birthday = new DateTime(2000, 1, 1), UserId = 5,
                         Email = "pedro.rodriguez@example.com", Phone = "567-890-1234"
                     }
                             });

            // Instancia el repositorio con los mocks correctos
            _personRepository = new PersonRepository(mockConfigData.Object, mockDataService.Object, mockDbConnection.Object);
        }

        [Fact]
        public async Task GetPeopleAsync_ShouldReturnListOfPeople()
        {
            // Datos de prueba
            var people = new List<PersonDTO>
    {
        new PersonDTO
        {
            PersonId = 1, IdentificationId = 1, IdentificationNumber = "A12345678", ClientName = "Juan",
            ClientLastName = "Pérez", GenderId = 1, Birthday = new DateTime(1985, 4, 12), UserId = 1,
            Email = "juan.perez@example.com", Phone = "123-456-7890"
        },
        new PersonDTO
        {
            PersonId = 2, IdentificationId = 2, IdentificationNumber = "B98765432", ClientName = "Ana",
            ClientLastName = "Gómez", GenderId = 2, Birthday = new DateTime(1990, 6, 22), UserId = 2,
            Email = "ana.gomez@example.com", Phone = "234-567-8901"
        },
        new PersonDTO
        {
            PersonId = 3, IdentificationId = 3, IdentificationNumber = "C11223344", ClientName = "Carlos",
            ClientLastName = "Lopez", GenderId = 1, Birthday = new DateTime(1982, 11, 30), UserId = 3,
            Email = "carlos.lopez@example.com", Phone = "345-678-9012"
        },
        new PersonDTO
        {
            PersonId = 4, IdentificationId = 4, IdentificationNumber = "D44332211", ClientName = "María",
            ClientLastName = "Martínez", GenderId = 2, Birthday = new DateTime(1995, 3, 14), UserId = 4,
            Email = "maria.martinez@example.com", Phone = "456-789-0123"
        },
        new PersonDTO
        {
            PersonId = 5, IdentificationId = 5, IdentificationNumber = "E55667788", ClientName = "Pedro",
            ClientLastName = "Rodríguez", GenderId = 1, Birthday = new DateTime(2000, 1, 1), UserId = 5,
            Email = "pedro.rodriguez@example.com", Phone = "567-890-1234"
        }
    };

            // Act
            var result = await _personRepository.GetPeopleAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PersonDTO>>(result);
            Assert.Equal(5, result.Count);
            Assert.Equal("Juan", result[0].ClientName);
            Assert.Equal("Ana", result[1].ClientName);
            Assert.Equal("Carlos", result[2].ClientName);
            Assert.Equal("María", result[3].ClientName);
            Assert.Equal("Pedro", result[4].ClientName);

            // Validar otros campos si es necesario
            Assert.Equal("Pérez", result[0].ClientLastName);
            Assert.Equal(1, result[0].GenderId);
            Assert.Equal(new DateTime(1985, 4, 12), result[0].Birthday);
            Assert.Equal("juan.perez@example.com", result[0].Email);
            Assert.Equal("123-456-7890", result[0].Phone);
        }

    }
}
