using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIUserValidation.Controllers.V1
{
    public class AuthenticationController : Controller
    {

        private readonly UserDbContext _dbContext;
        private readonly string _secretKey;

        public AuthenticationController(UserDbContext dbContext, IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]; // Se usa una clave secreta fija del appsettings.json
            _dbContext = dbContext;
        }


        //[HttpPost("token")]
        public async Task<IActionResult> ActionResult([FromBody] UserInfoME user)
        {
            // Verifica si el usuario existe en la base de datos
            var userExists = await _dbContext.UserInfo.AnyAsync(u => u.UserName == user.UserName);

            if (userExists)
            {
                // Crea un objeto que deseas devolver como JSON
                var response = new
                {
                    Message = "Operación exitosa",
                    Data = new { /* Aquí puedes incluir tus datos adicionales */ }
                };

                // Devuelve el objeto como JSON
                return Ok(response);
            }
            else
            {
                // Devuelve un error si el registro no existe
                return NotFound(new { Message = "Usuario no encontrado." });
            }
        }


        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] UserInfoME user)
        {
            // Verifica si el usuario existe en la base de datos
            var validUser = await _dbContext.UserInfo
                .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);

            if (validUser != null)
            {
                // Crea los claims del usuario (puedes agregar más claims si lo necesitas)
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, validUser.UserName),
          
        };

                var tokenHandler = new JwtSecurityTokenHandler();
                var byteKey = Encoding.UTF8.GetBytes(_secretKey); // _secretKey es tu clave secreta, asegúrate de que tenga al menos 32 caracteres.

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Devuelve el token junto con el mensaje de éxito
                return Ok(new
                {
                    Token = tokenString,
                    Message = "Operación exitosa"
                });
            }
            else
            {
                // Devuelve un error si el usuario no existe
                return NotFound(new { Message = "Usuario no encontrado." });
            }
        }
    }
}