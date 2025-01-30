using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using APIUserValidation.Helpers;
using DataAccess;
using DataAccess.DataAccessClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Security.Claims;

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
            _configurationData = configurationData; // Asignación correcta
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
                var clients = await _personRepository.GetClientsAsync(); // Asegúrate de usar await aquí
                return new JsonResult(clients); // Usa JsonResult para devolver datos en formato JSON
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Manejo de error
            }
        }

        [AllowAnonymous]
        [HttpPost("CreateClientsAsync")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.CreateUserSummary,
        Description = SwaggerComments.Clients.CreateUserDescription)]
        public async Task<IActionResult> CreateClientsAsync(PersonDTO person)
        {
            try
            {
                // Usa 'await' para esperar la ejecución del método asíncrono
                await _personRepository.CreateAsync(person);

                // Si todo va bien, devuelve un mensaje de éxito
                return Ok(new { message = "Client created successfully." });
            }
            catch (Exception ex)
            {
                // Si ocurre un error, lo devuelve en formato BadRequest
                return BadRequest(new { message = ex.Message });
            }
        }

        //[Authorize]
        //[HttpGet("GetClientByID{id}")]
        //[SwaggerOperation(
        //Summary = SwaggerComments.Clients.GetUserByIdSummary,
        //Description = SwaggerComments.Clients.GetUserByIdDescription)]
        //public ActionResult GetClientById(int id)
        //{
        //    try
        //    {
        //        var client = _clientsRepository.GetClientById(id);
        //        if (client == null) return NotFound(); // Manejo de cliente no encontrado

        //        return Ok(client);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log el error si es necesario
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpPost("CreateClient")]
        //[SwaggerOperation(
        //Summary = SwaggerComments.Clients.CreateUserSummary,
        //Description = SwaggerComments.Clients.CreateUserDescription)]
        //public ActionResult CreateClient([FromBody] ClientME client)
        //{
        //    try
        //    {
        //        var createdClient = _clientsRepository.CreateClient(client);
        //        return Ok(createdClient);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log el error si es necesario
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}


        //[HttpPut("ModifyClient")]
        //[SwaggerOperation(
        //Summary = SwaggerComments.Clients.UpdateUserSummary,
        //Description = SwaggerComments.Clients.UpdateUserDescription)]
        //public ActionResult ModifyClient([FromBody] ClientME client)
        //{
        //    try
        //    {
        //        var modifiedClient = _clientsRepository.ModifyClient(client);
        //        return Ok(modifiedClient);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log el error si es necesario
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        //[HttpDelete("DeleteClient{id}")]
        //[SwaggerOperation(
        //Summary = SwaggerComments.Clients.DeleteUserSummary,
        //Description = SwaggerComments.Clients.DeleteUserDescription)]
        //public ActionResult DeleteClient(int id)
        //{
        //    try
        //    {
        //        var result = _clientsRepository.DeleteClient(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log el error si es necesario
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

    }
}
