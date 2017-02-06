using System.Data.Entity;
namespace Practice.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<User> User { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Response> Response { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //        .HasOptional<Response>(r => r.Response)
            //        .WithMany(r => r.User)
            //        .HasForeignKey(r => r.ResponseId);

            //modelBuilder.Entity<Project>()
            //        .HasOptional<Response>(r => r.Response)
            //        .WithMany(r => r.Project)
            //        .HasForeignKey(r => r.ResponseId);
        }
    }

    
}