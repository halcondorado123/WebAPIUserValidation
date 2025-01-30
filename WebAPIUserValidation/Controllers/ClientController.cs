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
        private readonly IClientsRepository _clientsRepository;
        private readonly ConfigurationData _configurationData;

        public ClientController(IClientsRepository clientRepository, ConfigurationData configurationData)
        {
            _clientsRepository = clientRepository;
            _configurationData = configurationData; // Asignación correcta
        }

        private ActionResult ExecuteWithExceptionHandling(Func<ActionResult> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("GetClients")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.GetAllUsersSummary,
        Description = SwaggerComments.Clients.GetAllUsersDescription)]

        public ActionResult GetClients()
        {
            try
            {
                var clients = _clientsRepository.GetClients();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                // Log el error si es necesario
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetClientByID{id}")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.GetUserByIdSummary,
        Description = SwaggerComments.Clients.GetUserByIdDescription)]
        public ActionResult GetClientById(int id)
        {
            try
            {
                var client = _clientsRepository.GetClientById(id);
                if (client == null) return NotFound(); // Manejo de cliente no encontrado

                return Ok(client);
            }
            catch (Exception ex)
            {
                // Log el error si es necesario
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateClient")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.CreateUserSummary,
        Description = SwaggerComments.Clients.CreateUserDescription)]
        public ActionResult CreateClient([FromBody] ClientME client)
        {
            try
            {
                var createdClient = _clientsRepository.CreateClient(client);
                return Ok(createdClient);
            }
            catch (Exception ex)
            {
                // Log el error si es necesario
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("ModifyClient")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
        public ActionResult ModifyClient([FromBody] ClientME client)
        {
            try
            {
                var modifiedClient = _clientsRepository.ModifyClient(client);
                return Ok(modifiedClient);
            }
            catch (Exception ex)
            {
                // Log el error si es necesario
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteClient{id}")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.DeleteUserSummary,
        Description = SwaggerComments.Clients.DeleteUserDescription)]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                var result = _clientsRepository.DeleteClient(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log el error si es necesario
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
