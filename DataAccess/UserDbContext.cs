using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<ClientME>? Client { get; set; }
        public DbSet<GenreME>? Gender { get; set; }
        public DbSet<IdentificationME>? Identification { get; set; }
        public DbSet<IdentificationME>? IdType { get; set; }
        public DbSet<RelationShME>? Relationship { get; set; }
        public DbSet<RoleME>? Role { get; set; }
        public DbSet<UserInfoME>? UserInfo { get; set; }
    }
}