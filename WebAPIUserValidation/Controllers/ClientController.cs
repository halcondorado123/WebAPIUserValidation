using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Persons;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using APIUserValidation.Helpers.SwaggerComments.ClientControlles;
using APIUserValidation.Helpers.SwaggerComments.UserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace APIUserValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ConfigurationData _configurationData;

        public ClientController(IPersonRepository personRepository, ConfigurationData configurationData)
        {
            _personRepository = personRepository;
            _configurationData = configurationData;
        }

        //[AllowAnonymous]
        [HttpGet("GetClients")]
        [SwaggerOperation(
            Summary = SwaggerClientsCommentsSPA.Clients.GetClientsSummary,
            Description = SwaggerClientsCommentsSPA.Clients.GetClientsDescription)]
        public async Task<IActionResult> GetClients(int page = 1, int pageSize = 10)
        {
            try
            {
                var clients = await _personRepository.GetPeopleAsync(page, pageSize);
                if (clients == null || !clients.Any()) return NotFound(new { message = "No users found in the database." });

                return Ok(new { message = "Success", data = clients });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("GetClientByID{personId}")]
        [SwaggerOperation(
        Summary = SwaggerClientsCommentsSPA.Clients.GetClientsByIdSummary,
        Description = SwaggerClientsCommentsSPA.Clients.GetClientsByIdDescription)]
        public async Task<IActionResult> GetClientById(int personId)
        {
            try
            {
                var client = await _personRepository.GetPersonByIdAsync(personId);
                if (client == null)
                {
                    return NotFound(new { message = "No user found with the specified PersonId." });
                }

                return new JsonResult(client);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreatePerson")]
        [SwaggerOperation(
         Summary = SwaggerClientsCommentsSPA.Clients.CreateClientSummary,
         Description = SwaggerClientsCommentsSPA.Clients.CreateClientDescription)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDTO person)
        {
            try
            {
               var idCreated = await _personRepository.CreatePersonAsync(person);

                return Ok(new { message = "The client has been successfully created, the ID code is " + idCreated });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("BulkInsertPeople")]
        [SwaggerOperation(
         Summary = SwaggerClientsCommentsSPA.Clients.BulkInsertClientsSummary,
         Description = SwaggerClientsCommentsSPA.Clients.BulkInsertClientsDescription)]
        public async Task<IActionResult> BulkInsertPeople([FromBody] List<PersonDTO> people)
        {
            try
            {
                var createdIds = await _personRepository.BulkInsertPeopleAsync(people);
                var idsString = $"[{string.Join(", ", createdIds)}]";

                return Ok(new { message = $"Successfully inserted {createdIds.Count} people.", ids = idsString });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdatePerson")]
        [SwaggerOperation(
        Summary = SwaggerClientsCommentsSPA.Clients.UpdateClientSummary,
        Description = SwaggerClientsCommentsSPA.Clients.UpdateClientDescription)]
        public async Task<IActionResult> UpdatePerson([FromBody]PersonDTO person)
        {
            try
            {
                await _personRepository.UpdatePersonAsync(person);

                return Ok(new { message = "The client has been successfully updated." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeletePerson")]
        [SwaggerOperation(
        Summary = SwaggerClientsCommentsSPA.Clients.DeleteClientSummary,
        Description = SwaggerClientsCommentsSPA.Clients.DeleteClientDescription)]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            try
            {
                await _personRepository.DeletePersonAsync(personId);

                return Ok(new { message = "The client has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
