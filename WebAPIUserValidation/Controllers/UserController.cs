using ApiUserValidation.Data.DataAccess.Users;
using ApiUserValidation.Models.DTOs;
using APIUserValidation.Helpers.SwaggerComments.UserController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using LoginRequest = ApiUserValidation.Models.Entities.TokenME.LoginRequest;

namespace APIUserValidation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _IUsersRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _IUsersRepository = usersRepository;
            _configuration = configuration;
        }

        //[AllowAnonymous]
        [HttpGet("GetUsers")]
        [SwaggerOperation(
            Summary = SwaggerUsersCommentsENG.Users.GetUsersSummary,
            Description = SwaggerUsersCommentsENG.Users.GetUsersDescription)]
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
                return BadRequest(new { Success = false, message = ex.Message });
                //throw ExceptionHandler.HandleException(ex);
                //return StatusCode(500, $"Internal server error:\n\n {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        [SwaggerOperation(
            Summary = SwaggerUsersCommentsENG.Users.GetUsersByIdSummary,
            Description = SwaggerUsersCommentsENG.Users.GetUsersByIdDescription)]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _IUsersRepository.GetUserByIdAsync(id);
                if (user == null) return NotFound(new { message = "No user found with the specified PersonId." });

                return Ok(new { Success = true, message = "Success", data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpGet("GetUserByParameters")]
        [SwaggerOperation(
        Summary = SwaggerUsersCommentsENG.Users.GetUsersByParametersSummary,
        Description = SwaggerUsersCommentsENG.Users.GetUsersByParametersDescription)]
        public async Task<IActionResult> GetUserByParameters(int? userTypeId, string? userId, string? email)
        {
            try
            {
                var user = await _IUsersRepository.GetUserByParametersAsync(userTypeId, userId, email);
                if (user == null) return NotFound(new { message = "No user found with the specified PersonId." });

                return Ok(new { Success = true, message = "Success", data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPost("CreateUser")]
        [SwaggerOperation(
            Summary = SwaggerUsersCommentsENG.Users.CreateUserSummary,
            Description = SwaggerUsersCommentsENG.Users.CreateUserDescription)]
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
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPost("BulkInsertUsers")]
        [SwaggerOperation(
         Summary = SwaggerUsersCommentsENG.Users.BulkInsertUsersSummary,
         Description = SwaggerUsersCommentsENG.Users.BulkInsertUsersDescription)]
        public async Task<IActionResult> BulkInsertUsers([FromBody] List<UserCreateDTO> users)
        {
            try
            {
                var createdIds = await _IUsersRepository.BulkInsertUsersAsync(users);
                return Ok(new
                {
                    Success = true,
                    message = $"Successfully inserted {users.Count} users.",
                    ids = string.Join(", ", createdIds)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPut("InsertUserToExistingPerson")]
        [SwaggerOperation(
            Summary = SwaggerUsersCommentsENG.Users.InsertUserToExistingPersonSummary,
            Description = SwaggerUsersCommentsENG.Users.InsertUserToExistingPersonDescription)]
        public async Task<IActionResult> InsertUserToExistingPerson([FromBody] UserExistentDTO userDto)
        {
            try
            {
                var user = await _IUsersRepository.AddUserToExistingPersonAsync(userDto);
                if (user == null) return NotFound($"No user found with the specified PersonId.");

                return CreatedAtAction(nameof(GetUserById), new { id = user.PersonId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPut("UpdateUser")]
        [SwaggerOperation(
            Summary = SwaggerUsersCommentsENG.Users.UpdateUserSummary,
            Description = SwaggerUsersCommentsENG.Users.UpdateUserDescription)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO userDto)
        {
            try
            {
                var updatedUser = await _IUsersRepository.UpdateUserAsync(userDto);
                if (updatedUser == null) return NotFound(new { message = "The user was not found or there were no changes to the data." });

                return Ok(new { Success = true, message = "User succesfully uploaded.", user = updatedUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpDelete("DeletePerson")]
        [SwaggerOperation(
        Summary = SwaggerUsersCommentsENG.Users.DeleteUserSummary,
        Description = SwaggerUsersCommentsENG.Users.DeleteUserDescription)]
        public async Task<IActionResult> DeletePerson(int typeId, string identificationNumber)
        {
            try
            {
                int? deletedPersonId = await _IUsersRepository.DeleteUserAsync(typeId, identificationNumber);

                if (deletedPersonId == null)
                {
                    return BadRequest(new { Success = false, message = "No user found with the provided information." });
                }

                return Ok(new { Success = true, message = "The user has been successfully deleted.", DeletedPersonId = deletedPersonId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPost("Validate_User")]
        [SwaggerOperation(
        Summary = SwaggerUsersCommentsENG.Users.ValidateUserSummary,
        Description = SwaggerUsersCommentsENG.Users.ValidateUserDescription)]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequest request)
        {
            try
            {
                var user = await _IUsersRepository.ValidateUserAsync(request.UserName, request.Password);

                if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
                    return BadRequest(new { Success = false, message = "Username and password are required." });
                if (user == null)
                    return Unauthorized(new { Success = false, message = "Invalid username or password." });

                return Ok(new { Success = true, message = "Login successful", user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}

