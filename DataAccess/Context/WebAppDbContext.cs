
using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiUserValidation.Data.Context
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }
        public DbSet<PersonME> People { get; set; }
        public DbSet<UserME> Users { get; set; }
        public DbSet<GenderME>? Gender { get; set; }
        public DbSet<IdentificationME>? Identification { get; set; }
        public DbSet<RoleME>? Role { get; set; }
        public DbSet<StatusME>? Status { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esquema por defecto
            modelBuilder.HasDefaultSchema("UVA");

            // Configuración de tablas específicas
            modelBuilder.Entity<UserME>()
                .ToTable("UserME", "UVA") // Especificamos el esquema y nombre de la tabla
                .HasBaseType<PersonME>();  // Configuramos la herencia

            // Configuración del campo PersonId como IDENTITY
            modelBuilder.Entity<PersonME>()
                .Property(p => p.PersonId)
                .ValueGeneratedOnAdd();  // Esto es suficiente para que el valor sea generado automáticamente

            // Generación automática de IDs para otras tablas
            modelBuilder.Entity<GenderME>()
                .Property(e => e.GenderId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<IdentificationME>()
                .Property(e => e.IdentificationId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<RoleME>()
                .Property(e => e.RolID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<StatusME>()
                .Property(e => e.StatusId)
                .ValueGeneratedOnAdd();
        }


    }
}