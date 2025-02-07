using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using APIUserValidation.Helpers;
using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = ApiUserValidation.Models.Entities.LoginRequest;

namespace APIUserValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _IUsersRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _IUsersRepository = usersRepository;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _IUsersRepository.GetUsersAsync(); // Debes tener este método en tu repositorio

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{id}")]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _IUsersRepository.GetUserByIdAsync(id); // Debes tener este método en tu repositorio

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            try
            {
                // Llamas al método CreateUserAsync del repositorio
                var createdUser = await _IUsersRepository.CreateUserAsync(userDto);

                // Devuelves una respuesta con el usuario creado
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:\n\n {ex.Message}");
            }
        }


        [AllowAnonymous]
        [HttpPost("BulkInsertUsers")]
        [SwaggerOperation(
         Summary = SwaggerComments.Clients.CreateUserSummary,
         Description = SwaggerComments.Clients.CreateUserDescription)]
        public async Task<IActionResult> BulkInsertUsers([FromBody] List<UserCreateDTO> users)
        {
            try
            {
                var createdIds = await _IUsersRepository.BulkInsertUsersAsync(users);
                return Ok(new { message = $"Successfully inserted {users.Count} people.", ids = createdIds });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("Insert_user_to_existing_person")]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> InsertUserToExistingPerson([FromBody] UserCreateDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Datos de usuario inválidos.");
            }

            var user = await _IUsersRepository.AddUserToExistingPersonAsync(userDto);

            if (user == null)
            {
                return NotFound($"No se encontró la persona con ID");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.PersonId }, user);
        }


        [HttpPut("Update_user")]
        [Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser([FromBody] UserCreateDTO userDto)
        {
            try
            {
                // Llamamos al repositorio para actualizar y obtener el usuario actualizado
                var updatedUser = await _IUsersRepository.UpdateUserAsync(userDto);

                if (updatedUser == null)
                {
                    return NotFound(new { message = "No se encontró el usuario o no hubo cambios en los datos." });
                }

                return Ok(new
                {
                    message = "Usuario actualizado correctamente.",
                    user = updatedUser 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error al actualizar el usuario.",
                    error = ex.Message
                });
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
                await _IUsersRepository.DeleteUserAsync(personId);

                return Ok(new { message = "The client has been successfully deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("Validate_User")]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest(new { message = "Username and password are required." });

            try
            {
                var user = await _IUsersRepository.ValidateUserAsync(request.UserName, request.Password);

                if (user == null)
                    return Unauthorized(new { message = "Invalid username or password." });

                return Ok(new { message = "Login successful", user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An internal error occurred.", details = ex.Message });
            }
        }
    }
}

