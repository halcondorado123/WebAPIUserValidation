using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APIUserValidation.Modules.Mapper;
using ApiUserValidation.Data.Context;
using ApiUserValidation.Data.Configuration;
using ApiUserValidation.Data.DataAccess.Persons;
using ApiUserValidation.Services.Services;
using ApiUserValidation.Data.DataAccess.Users;
using DataAccess.DataAccessUsers;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddCors();
builder.Services.AddSingleton(new ConfigurationData(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configuración de la base de datos
builder.Services.AddDbContext<WebAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"),
        b => b.MigrationsAssembly("APIUserValidation")));


// Configuración de repositorios y servicios
//builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddControllers();

// MODULES
builder.Services.AddSwaggerServices();
builder.Services.AddMapper();



// 🔹 Verificar que la clave JWT no sea nula
var jwtKey = builder.Configuration["JwtConfig:Key"];

if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
{
    throw new InvalidOperationException("JWT Key is missing or too short in configuration.");
}

var key = Encoding.UTF8.GetBytes(jwtKey);

// 🔹 Configuración de autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero // 🔹 Evita problemas de sincronización de tiempo en la validación
    };
});

builder.Services.AddAuthorization();

// Crear la aplicación
var app = builder.Build();

// Llamar a la inicialización de la base de datos
await DatabaseInitializer.SeedDataAsync(app.Services);

// Configura el middleware de autenticación personalizado antes de otros middlewares
app.UseMiddleware<CustomAuthenticationMiddleware>();

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
