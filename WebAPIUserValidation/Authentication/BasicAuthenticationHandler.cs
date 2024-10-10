using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace ContractInProcessAPI.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IConfiguration _configuration;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IOptions<UserInfoME> authenticationSettings,
            IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                var credentials = GetCredentialsFromHeader(authHeader);
                var username = credentials[0];
                var password = credentials[1];

                var validUsers = _configuration.GetSection("UserME:Users").Get<List<UserInfoME>>();
                var user = validUsers.FirstOrDefault(u => u.UserName == username && u.UserPassword == password);

                if (user == null)
                {
                    return AuthenticateResult.Fail("Invalid credentials");
                }

                // Si las credenciales son válidas, generamos el token JWT
                var token = GenerateJwtToken(username);

                var claims = new[] { new Claim(ClaimTypes.Name, username), new Claim("Token", token) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);

                return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"Error: {ex.Message}");
            }
        }

        private string[] GetCredentialsFromHeader(string header)
        {
            var encodedCredentials = header.Substring("Basic ".Length).Trim();
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

            if (credentials.Length != 2)
            {
                throw new FormatException("Invalid credentials format");
            }

            return credentials;
        }

        // Método para generar el token JWT
        private string GenerateJwtToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1), // El token expirará en 1 hora
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Devolvemos el token como string
        }
    }
}
