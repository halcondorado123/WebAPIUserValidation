using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.TokenME;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIUserValidation.Controllers.V1
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

            [HttpPost("login")]
            [AllowAnonymous]
            public dynamic LoginUser([FromBody] object optData)
            {
                var data = JsonConvert.DeserializeObject<UserInfoME>(optData.ToString());

                List<UserInfoME> users = _IUsersRepository.GetUsers();

                UserInfoME user = users.FirstOrDefault(x => x.UserName == data.UserName && x.UserPassword == data.UserPassword);

                if (user == null)
                {
                    return new
                    {
                        success = false,
                        message = "Credenciales inválidas",
                        result = ""
                    };
                }

                var jwt = _configuration.GetSection("Jwt").Get<JWTokenME>();

                var claims = new Claim[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("userId", user.UsuId.ToString()),
            new Claim("userName", user.UserName),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: signin
                );

                return new
                {
                    success = true,
                    message = "Ingreso exitoso",
                    result = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
        }


    }
}
