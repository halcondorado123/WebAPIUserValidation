using Microsoft.OpenApi.Models;

public static class SwaggerConfig
{
    // Método para agregar servicios de Swagger
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserValidation V1", Version = "Versio1.0" });

            // Configuración de JWT en Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Ingrese el token JWT en el formato: Bearer {tu_token_jwt}"
            });

            // Requerir el esquema de seguridad para todas las operaciones
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" // Aquí hacemos referencia al esquema de seguridad
                        }
                    },
                    new string[] {}
                }
            });
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
            //c.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté en la raíz
        });
    }
}
