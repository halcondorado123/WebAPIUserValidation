using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIUserValidation.Controllers.V1
{
    public class AuthenticationController : Controller
    {

        private readonly UserDbContext _dbContext;
        private readonly string _secretKey;


        public AuthenticationController(UserDbContext dbContext, IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:SecretKey"]; // Asegúrate de que esta clave esté en tu appsettings.json
            _dbContext = dbContext;
        }

        // Método para generar una clave secreta dinámica
        private string GenerateRandomKey(int size)
        {
            var key = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return Convert.ToBase64String(key);
        }

        [Authorize]
        [HttpPost("token")]
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


        [HttpPost("ValidarUsuarioExistente")]
        public async Task<IActionResult> GenerateToken([FromBody] UserInfoME user)
        {
            var validUser = await _dbContext.UserInfo
                .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);

            if (validUser == null)
            {
                return Unauthorized(new { Message = "Credenciales no válidas." });
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, validUser.UserName),
        };

            // Generar la clave secreta dinámica
            var secretKey = GenerateRandomKey(32); // 32 bytes = 256 bits
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), SecretKey = secretKey });
        }


        //[HttpPost("login")]
        //public IActionResult Login([FromHeader] string authorization)
        //{
        //    if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Basic "))
        //    {
        //        return Unauthorized("Authorization header missing or invalid.");
        //    }

        //    try
        //    {
        //        // Obtener las credenciales del encabezado
        //        var credentials = GetCredentialsFromHeader(authorization);
        //        var username = credentials[0];
        //        var password = credentials[1];

        //        // Aquí puedes validar el usuario y la contraseña de forma más robusta
        //        if (username == "admin" && password == "password123") // Cambiar por lógica de validación real
        //        {
        //            var token = GenerateJwtToken(username);
        //            return Ok(new { token });
        //        }

        //        return Unauthorized("Credenciales incorrectas.");
        //    }
        //    catch (FormatException)
        //    {
        //        return BadRequest("Formato de credenciales inválido.");
        //    }
        //}

        //private string[] GetCredentialsFromHeader(string header)
        //{
        //    var encodedCredentials = header.Substring("Basic ".Length).Trim();
        //    var credentialBytes = Convert.FromBase64String(encodedCredentials);
        //    var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

        //    if (credentials.Length != 2)
        //    {
        //        throw new FormatException("Invalid credentials format");
        //    }

        //    return credentials;
        //}
    }
}