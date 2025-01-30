using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributesME;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<ClientME> Clients { get; set; }
        public DbSet<PersonME> People { get; set; }
        public DbSet<UserInfoME>? UserInfo { get; set; }
        public DbSet<GenderME>? Gender { get; set; }
        public DbSet<IdentificationME>? Identification { get; set; }
        public DbSet<RelationShME>? Relationship { get; set; }
        public DbSet<RoleME>? Role { get; set; }
        public DbSet<StatusME>? Status { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("UVA"); // O el esquema que desees

            modelBuilder.Entity<ClientME>()
                .HasOne(c => c.Role)
                .WithMany()  // Relación de muchos a uno
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Restrict);  // Asegura que no se elimine en cascada

            modelBuilder.Entity<ClientME>()
                .HasOne(c => c.UserInfo)
                .WithMany()  // Relación de muchos a uno
                .HasForeignKey(c => c.UserInfoId)
                .OnDelete(DeleteBehavior.SetNull);  // No eliminar en cascada

            modelBuilder.Entity<UserInfoME>()
                .HasOne(u => u.Person)
                .WithOne(p => p.UserInfo)  // Relación de uno a uno
                .HasForeignKey<UserInfoME>(u => u.PersonId)
                .OnDelete(DeleteBehavior.SetNull);  // Esto evitaría la eliminación en cascada

            modelBuilder.Entity<UserInfoME>()
                .HasOne(u => u.Person)
                .WithOne(p => p.UserInfo)  // Relación de uno a uno
                .HasForeignKey<UserInfoME>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);  // Eliminar en cascada al eliminar la persona

            modelBuilder.Entity<GenderME>()
           .Property(e => e.GenderId)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

            modelBuilder.Entity<IdentificationME>()
           .Property(e => e.IdentificationId)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

            modelBuilder.Entity<RelationShME>()
           .Property(e => e.RelatId)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

            modelBuilder.Entity<RoleME>()
           .Property(e => e.RolID)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

            modelBuilder.Entity<StatusME>()
           .Property(e => e.StatusId)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

        }
    }
}