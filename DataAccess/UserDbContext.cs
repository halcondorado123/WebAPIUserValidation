using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.UserAttributes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<ClientME>? Client { get; set; }
        public DbSet<GenderME>? Gender { get; set; }
        public DbSet<IdentificationME>? Identification { get; set; }
        public DbSet<IdentificationME>? IdType { get; set; }
        public DbSet<RelationShME>? Relationship { get; set; }
        public DbSet<RoleME>? Role { get; set; }
        public DbSet<UserInfoME>? UserInfo { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ClientME>()
           .Property(e => e.ClientId)
           .ValueGeneratedOnAdd(); // Esto asegura que se genere automáticamente

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

        }

    }


}