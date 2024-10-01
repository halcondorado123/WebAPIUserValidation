using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class ValidationDbContext : DbContext
    {
        public ValidationDbContext(DbContextOptions<ValidationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<ClientME>? Client { get; set; }
        public DbSet<GenreME>? Gender { get; set; }
        public DbSet<IdClientME>? Identification { get; set; }
        public DbSet<IdentificationME>? IdType { get; set; }
        public DbSet<RelationShME>? Relationship { get; set; }
        public DbSet<RoleME>? Role { get; set; }
        public DbSet<UserInfoME>? UserInfo { get; set; }
    }
}
