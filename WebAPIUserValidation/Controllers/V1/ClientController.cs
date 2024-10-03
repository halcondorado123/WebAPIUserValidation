using DataAccess;
using DataAccess.DataAccessClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.TokenME;
using System;
using System.Security.Claims;

namespace APIUserValidation.Controllers.V1
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

        [HttpGet]
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

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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
