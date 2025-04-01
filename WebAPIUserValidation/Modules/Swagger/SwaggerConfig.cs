using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerConfig
{
    // Método para agregar servicios de Swagger
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserValidation V1", Version = "Versio1.0" });
            c.EnableAnnotations();
            // Configuración de JWT en Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT en el formato, no es necesario escribir la palabra Token, solo ingrese el Token."
            });

            // Requerir el esquema de seguridad para todas las operaciones
            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
            });
            //c.OperationFilter<AddAuthResponses>();
        });
    }

    // Método para usar middleware de Swagger
    public static void UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger(); // Habilitar Swagger

        app.UseSwaggerUI(c =>
        {
            // Configurar el endpoint de Swagger
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserValidation V1");
        });
    }

    //public class AddAuthResponses : IOperationFilter
    //{
    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        if (operation.Responses.ContainsKey("401"))
    //        {
    //            operation.Responses["401"] = new OpenApiResponse
    //            {
    //                Description = "No autorizado. Inicia sesión para acceder.",
    //            };
    //        }
    //    }
    //}

}
