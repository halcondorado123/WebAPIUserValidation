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
    public class UserController : Controller
    {

        private readonly IUsersRepository _IUsersRepository;
        public IConfiguration _configuration;

        public UserController(IUsersRepository userRepository, IConfiguration configuration)
        {
            _IUsersRepository = userRepository;
            _configuration = configuration;
        }

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
            public ActionResult GetUsers()
            {
                try
                {
                    var users = _IUsersRepository.GetUsers();
                    return Ok(users);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpGet("{id}")]
            [Authorize]
            [AllowAnonymous]
            public ActionResult GetUserById(int id)
            {
                try
                {
                    var user = _IUsersRepository.GetUserById(id);
                    if (user == null) return NotFound(); // Manejo de usuario no encontrado

                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpPost]
            [Authorize]
            [AllowAnonymous]
            public ActionResult CreateUser([FromBody] UserInfoME user)
            {
                try
                {
                    var createdUser = _IUsersRepository.CreateUser(user);
                    return Ok(createdUser);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpPut]
            [Authorize]
            [AllowAnonymous]
            public ActionResult ModifyUser([FromBody] UserInfoME user)
            {
                try
                {
                    var modifiedUser = _IUsersRepository.UpdateUser(user);
                    return Ok(modifiedUser);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            [HttpDelete("{id}")]
            [Authorize]
            [AllowAnonymous]
            public ActionResult DeleteUser(int id)
            {
                try
                {
                    var result = _IUsersRepository.DeleteUser(id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }
        }
    }
}
