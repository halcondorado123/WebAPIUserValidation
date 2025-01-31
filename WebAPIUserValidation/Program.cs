using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIUserValidation.Modules.Mapper;
using ApiUserValidation.Data.DataAccess.Clients;
using ApiUserValidation.Data.Context;
using ApiUserValidation.Data.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddCors();
builder.Services.AddSingleton(new ConfigurationData(builder.Configuration.GetConnectionString("SQLConnection")));

// Configuración de la base de datos
builder.Services.AddDbContext<WebAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        b => b.MigrationsAssembly("APIUserValidation")));




// Configuración de repositorios y servicios
//builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddControllers();

// MODULES
builder.Services.AddSwaggerServices();
builder.Services.AddMapper();

// Configuración de autenticación JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; // Cambia a true en producción
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Crear la aplicación
var app = builder.Build();

// Llamar a la inicialización de la base de datos
await DatabaseInitializer.SeedDataAsync(app.Services);

// Configuración del middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Llamar al método para usar el middleware de Swagger desde el helper
    app.UseSwaggerMiddleware();
}

// Habilitar CORS
app.UseCors(option =>
{
    option.AllowAnyMethod();
    option.AllowAnyHeader();
    option.AllowAnyOrigin();
});

// Configuración del middleware HTTPS y rutas
app.UseHttpsRedirection();
app.UseRouting();

// Configuración de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.Run();
