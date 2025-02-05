using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        //[Authorize]
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
        //[Authorize]
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
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
                int affectedRows = await _IUsersRepository.UpdateUserAsync(userDto);
                if (affectedRows > 0)
                {
                    return Ok(new { message = "Usuario actualizado correctamente." });
                }
                else
                {
                    return NotFound(new { message = "No se encontraron registros para actualizar." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al actualizar el usuario.", error = ex.Message });
            }
        }

        //[HttpDelete("{id}")]
        //[Authorize]
        //[AllowAnonymous]
        //public ActionResult DeleteUser(int id)
        //{
        //    try
        //    {
        //        var result = _IUsersRepository.DeleteUser(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}
    }
}

