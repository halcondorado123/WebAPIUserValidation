using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Data.Exceptions;
using ApiUserValidation.Models.DTOs;
using ApiUserValidation.Models.Entities;
using APIUserValidation.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> GetUsers(int page = 1, int pageSize = 10)
        {
            try
            {
                var users = await _IUsersRepository.GetUsersAsync(page, pageSize);
                if (users == null || !users.Any()) return NotFound(new { message = "No users found in the database." });

                return Ok(new { message = "Success", data = users });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
                //return StatusCode(500, $"Internal server error:\n\n {ex.Message}");
                //return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _IUsersRepository.GetUserByIdAsync(id);
                if (user == null) return NotFound(new { message = "No user found with the specified PersonId." });

                return Ok(new { message = "Success", data = user });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        [HttpPost]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            try
            {
                var createdUser = await _IUsersRepository.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById),
                    new { id = createdUser }, createdUser);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
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
                var idsString = $"[{string.Join(", ", createdIds)}]";
                return Ok(new { message = $"Successfully inserted {users.Count} users.", ids = createdIds });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        [HttpPut("Insert_user_to_existing_person")]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> InsertUserToExistingPerson([FromBody] UserCreateDTO userDto)
        {
            try
            {
                var user = await _IUsersRepository.AddUserToExistingPersonAsync(userDto);
                if (user == null) return NotFound($"No user found with the specified PersonId.");

                return CreatedAtAction(nameof(GetUserById), new { id = user.PersonId }, user);
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        [HttpPut("Update_user")]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = SwaggerComments.Clients.GetAllUsersSummary,
            Description = SwaggerComments.Clients.GetAllUsersDescription)]
        public async Task<IActionResult> UpdateUser([FromBody] UserCreateDTO userDto)
        {
            try
            {
                var updatedUser = await _IUsersRepository.UpdateUserAsync(userDto);
                if (updatedUser == null) return NotFound(new { message = "The user was not found or there were no changes to the data." });

                return Ok(new { message = "User succesfully uploaded.", user = updatedUser });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        [HttpDelete("DeletePerson")]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
        public async Task<IActionResult> DeletePerson(int personId)
        {
            try
            {
                await _IUsersRepository.DeleteUserAsync(personId);
                return Ok(new { message = "The user has been successfully deleted." });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }

        [HttpPost("Validate_User")]
        [Authorize]
        [AllowAnonymous]
        [SwaggerOperation(
        Summary = SwaggerComments.Clients.UpdateUserSummary,
        Description = SwaggerComments.Clients.UpdateUserDescription)]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _IUsersRepository.ValidateUserAsync(request.UserName, request.Password);

                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { message = "Username and password are required." });
                if (user == null)
                    return Unauthorized(new { message = "Invalid username or password." });

                return Ok(new { message = "Login successful", user });
            }
            catch (Exception ex)
            {
                throw ExceptionHandler.HandleException(ex);
            }
        }
    }
}

