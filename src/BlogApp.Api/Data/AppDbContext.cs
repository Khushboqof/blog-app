using BlogApp.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; } = null!;

        public virtual DbSet<BlogPost> BlogPosts { get; set; } = null!;


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(o => o.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(o => o.Username).IsUnique();

            modelBuilder.Entity<BlogPost>()
                .HasOne<User>(s => s.User)
                .WithMany(o => o.BlogPosts)
                .HasForeignKey(o => o.UserId)   
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
        }

       

    }
}
