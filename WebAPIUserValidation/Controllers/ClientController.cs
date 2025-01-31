using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Persons;
using ApiUserValidation.Models.DTOs;
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
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                var clients = await _personRepository.GetPeopleAsync();
                return new JsonResult(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("GetClientByID{personId}")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.GetUserByIdSummary,
        Description = SwaggerComments.Clients.GetUserByIdDescription)]
        public async Task<IActionResult> GetClientById(int personId)
        {
            try
            {
                var client = await _personRepository.GetPersonByIdAsync(personId);
                if (client == null) return NotFound();

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
         Summary = SwaggerComments.Clients.CreateUserSummary,
         Description = SwaggerComments.Clients.CreateUserDescription)]
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
        [HttpPut("UpdatePerson")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
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
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
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
