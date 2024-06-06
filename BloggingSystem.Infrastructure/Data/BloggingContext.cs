using Microsoft.EntityFrameworkCore;
using BloggingSystem.Domain.Entities;

namespace BloggingSystem.Infrastructure
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations can go here
        }
    }
}
