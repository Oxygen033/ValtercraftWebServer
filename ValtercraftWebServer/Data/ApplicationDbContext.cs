using Microsoft.EntityFrameworkCore;
using ValtercraftWebServer.Models;

namespace ValtercraftWebServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WhiteListRequest>()
                .HasOne(w => w.User)
                .WithMany(u => u.WhiteListRequests)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<WhiteListRequest> WhiteListRequests { get; set; }

    }
}
