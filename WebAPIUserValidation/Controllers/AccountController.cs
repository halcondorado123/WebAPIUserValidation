using ApiUserValidation.Data.Context;
using ApiUserValidation.Models.Entities.TokenME;
using ApiUserValidation.Services.Services;
using APIUserValidation.Helpers.SwaggerComments.AccountController;
using APIUserValidation.Helpers.SwaggerComments.ClientControlles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace APIUserValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly JwtService _jwtservice;
        private readonly WebAppDbContext _dbContext;

        public AccountController(JwtService jwtservice, WebAppDbContext dbContext)
        {
            _jwtservice = jwtservice;
            _dbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(
            Summary = SwaggerTokenGenerateCommentSPA.UserAuthorization.UserAccountTokenSummary,
            Description = SwaggerTokenGenerateCommentSPA.UserAuthorization.UserAccountTokenDescription)]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequest request)
        {
            // Llamar al servicio de autenticación
            var result = await _jwtservice.Authenticate(request);

            // Si el servicio retorna null, significa que las credenciales son incorrectas
            if (result == null)
            {
                return Unauthorized(); // Retorna 401 si las credenciales son incorrectas
            }
            // Si la autenticación es exitosa, retorna el token JWT
            return Ok(result); // Retorna 200 con el objeto LoginResponseModel
        }

    }
}
