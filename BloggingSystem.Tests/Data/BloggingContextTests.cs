using Xunit;
using BloggingSystem.Infrastructure;
using BloggingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BloggingSystem.Tests.Data
{
    public class BloggingContextTests
    {
        private readonly BloggingContext _context;

        public BloggingContextTests()
        {
            var options = new DbContextOptionsBuilder<BloggingContext>()
                .UseInMemoryDatabase(databaseName: "BloggingTestDb")
                .Options;
            _context = new BloggingContext(options);
        }

        [Fact]
        public void CanAddPost()
        {
            var post = new Post { Title = "Test Title", Content = "Test Content", AuthorId = 1 };
            _context.Posts.Add(post);
            _context.SaveChanges();

            var savedPost = _context.Posts.FirstOrDefault(p => p.Title == "Test Title");
            Assert.NotNull(savedPost);
        }

        [Fact]
        public void CanAddAuthor()
        {
            var author = new Author { Name = "Test Author" };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var savedAuthor = _context.Authors.FirstOrDefault(a => a.Name == "Test Author");
            Assert.NotNull(savedAuthor);
        }

        [Fact]
        public void CanRetrievePostWithAuthor()
        {
            var author = new Author { Name = "Test Author" };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var post = new Post { Title = "Test Title", Content = "Test Content", AuthorId = author.Id };
            _context.Posts.Add(post);
            _context.SaveChanges();

            var savedPost = _context.Posts.Include(p => p.Author).FirstOrDefault(p => p.Title == "Test Title");
            Assert.NotNull(savedPost);
            Assert.NotNull(savedPost.Author);
        }
    }
}
