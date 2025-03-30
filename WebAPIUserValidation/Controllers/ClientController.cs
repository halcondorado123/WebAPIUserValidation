using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Persons;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using APIUserValidation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace APIUserValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ConfigurationData _configurationData;

        public ClientController(IPersonRepository personRepository, ConfigurationData configurationData)
        {
            _personRepository = personRepository;
            _configurationData = configurationData;
        }

        [AllowAnonymous]
        [HttpGet("GetClients")]
        //[SwaggerOperation(
        //    Summary = SwaggerCommentsENG.Clients.GetAllUsersSummary,
        //    Description = SwaggerCommentsENG.Clients.GetAllUsersDescription)]
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

        [AllowAnonymous]
        [HttpGet("GetClientByID{personId}")]
        //[SwaggerOperation(
        //Summary = SwaggerCommentsENG.Clients.GetUserByIdSummary,
        //Description = SwaggerCommentsENG.Clients.GetUserByIdDescription)]
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

        [AllowAnonymous]
        [HttpPost("CreatePerson")]
        [SwaggerOperation(
         Summary = SwaggerCommentsENG.Clients.CreateUserSummary,
         Description = SwaggerCommentsENG.Clients.CreateUserDescription)]
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


        [AllowAnonymous]
        [HttpPost("BulkInsertPeople")]
        [SwaggerOperation(
         Summary = SwaggerCommentsENG.Clients.CreateUserSummary,
         Description = SwaggerCommentsENG.Clients.CreateUserDescription)]
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



        [AllowAnonymous]
        [HttpPut("UpdatePerson")]
        [SwaggerOperation(
        Summary = SwaggerCommentsENG.Clients.UpdateUserSummary,
        Description = SwaggerCommentsENG.Clients.UpdateUserDescription)]
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

        [AllowAnonymous]
        [HttpDelete("DeletePerson")]
        [SwaggerOperation(
        Summary = SwaggerCommentsENG.Clients.DeleteUserSummary,
        Description = SwaggerCommentsENG.Clients.UpdateUserDescription)]
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
