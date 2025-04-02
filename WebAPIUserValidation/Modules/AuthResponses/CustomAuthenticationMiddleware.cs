using Newtonsoft.Json;

public class CustomAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public CustomAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        // Si el resultado es un Unauthorized (401), personalizamos la respuesta
        if (context.Response.StatusCode == 401)
        {
            // Personalizamos el cuerpo de la respuesta con un mensaje JSON
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                message = "No tienes permiso para acceder a este recurso. Asegúrate de incluir un token JWT válido en la cabecera Authorization."
            }));
        }
    }
}
