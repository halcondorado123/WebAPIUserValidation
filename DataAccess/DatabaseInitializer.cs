using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributes;
using DataAccess;
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
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        // Aplicar migraciones si no están aplicadas
        await context.Database.MigrateAsync();

        // Seed otros datos (Género, Identificación, Estado Civil)
        await SeedGendersAsync(context);
        await SeedIdentificationTypesAsync(context);
        await SeedRelationshipStatusesAsync(context);
        await SeedRolesAsync(context);
    }

    // 3️⃣ Seed de Géneros
    private static async Task SeedGendersAsync(UserDbContext context)
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
    private static async Task SeedIdentificationTypesAsync(UserDbContext context)
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

    // 6️⃣ Seed de Estado de Relación
    private static async Task SeedRelationshipStatusesAsync(UserDbContext context)
    {
        if (!context.Relationship.Any())
        {
            context.Relationship.AddRange(
                new RelationShME { RelationType = "Soltero" },
                new RelationShME { RelationType = "Comprometido" },
                new RelationShME { RelationType = "Casado" },
                new RelationShME { RelationType = "Viudo" },
                new RelationShME { RelationType = "Separado" },
                new RelationShME { RelationType = "Unión libre" }
            );
            await context.SaveChangesAsync();
        }
    }

    // 5️⃣ Seed de Estado Civil
    private static async Task SeedRolesAsync(UserDbContext context)
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


}
