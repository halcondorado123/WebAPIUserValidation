using ApiUserValidation.Data.Context;
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public static class DatabaseInitializer
{
    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApiUserValidation.Data.Context.WebAppDbContext>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        // Solo ejecutamos la migración si hay más de 1 migración pendiente
        if (pendingMigrations.Count() > 1)
        {
            await context.Database.MigrateAsync();
        }

        // Seed otros datos (Género, Identificación, Estado Civil)
        await SeedGendersAsync(context);
        await SeedIdentificationTypesAsync(context);
        await SeedRolesAsync(context);
        await SendStatusTypeAsync(context);
    }

    // 3️⃣ Seed de Géneros
    private static async Task SeedGendersAsync(ApiUserValidation.Data.Context.WebAppDbContext context)
    {
        if (!context.Gender.Any())
        {
            context.Gender.AddRange(
                new GenderME { GenderType = "Masculino" },
                new GenderME { GenderType = "Femenino" },
                new GenderME { GenderType = "No responde" }
            );
            await context.SaveChangesAsync();
        }
    }

    // 4️⃣ Seed de Tipos de Identificación
    private static async Task SeedIdentificationTypesAsync(ApiUserValidation.Data.Context.WebAppDbContext context)
    {
        if (!context.Identification.Any())
        {
            context.Identification.AddRange(
                new IdentificationME { IdentificationType = "Cedula de ciudadania" },
                new IdentificationME { IdentificationType = "Tarjeta de identidad" },
                new IdentificationME { IdentificationType = "Cédula de extranjeria" },
                new IdentificationME { IdentificationType = "Pasaporte" },
                new IdentificationME { IdentificationType = "Permiso especial de permanencia" }
            );
            await context.SaveChangesAsync();
        }
    }

    // 5️⃣ Seed de Estado Civil
    private static async Task SeedRolesAsync(ApiUserValidation.Data.Context.WebAppDbContext context)
    {
        if (!context.Role.Any())
        {
            context.Role.AddRange(
                new RoleME { RolType = "Admin" },
                new RoleME { RolType = "User" },
                new RoleME { RolType = "Guest" },
                new RoleME { RolType = "Editor" },
                new RoleME { RolType = "Supervisor" },
                new RoleME { RolType = "Manager" },
                new RoleME { RolType = "Customer" }
            );
            await context.SaveChangesAsync();
        }
    }


    private static async Task SendStatusTypeAsync(ApiUserValidation.Data.Context.WebAppDbContext context)
    {
        if (!context.Status.Any())
        {
            context.Status.AddRange(
                new StatusME { StatusType = "Activo" },
                new StatusME { StatusType = "Inactivo" },
                new StatusME { StatusType = "Suspendido" },
                new StatusME { StatusType = "Bloqueado" },
                new StatusME { StatusType = "Supervisor" },
                new StatusME { StatusType = "Baneado" }
            );
            await context.SaveChangesAsync();
        }
    }
}
