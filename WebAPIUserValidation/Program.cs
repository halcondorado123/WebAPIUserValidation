using DataAccess;
using DataAccess.DataAccessClients;
using DataAccess.DataAccessUsers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddCors();
builder.Services.AddSingleton(new ConfigurationData(builder.Configuration.GetConnectionString("SQLConnection")));

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjUserValidation", Version = "v1" });
});

// Crear la aplicación
var app = builder.Build();

// Configuración del middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjUserValidation v1"));
}

// Habilitar CORS
app.UseCors(option =>
{
    option.AllowAnyMethod();
    option.AllowAnyHeader();
});

// Configuración del middleware
app.UseHttpsRedirection();
app.UseRouting();

// Omitir la autenticación y autorización
// app.UseAuthentication(); // Eliminar esta línea
// app.UseAuthorization(); // Eliminar esta línea

app.MapControllers(); // Registra los endpoints

app.Run();
