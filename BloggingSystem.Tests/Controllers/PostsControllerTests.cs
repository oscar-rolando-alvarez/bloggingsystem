using Xunit;
using Moq;
using BloggingSystem.API.Controllers;
using BloggingSystem.Application.Interfaces;
using BloggingSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BloggingSystem.Tests.Controllers
{
    public class PostsControllerTests
    {
        private readonly Mock<IPostService> _mockPostService;
        private readonly PostsController _controller;

        public PostsControllerTests()
        {
            _mockPostService = new Mock<IPostService>();
            _controller = new PostsController(_mockPostService.Object);
        }

        [Fact]
        public async Task CreatePost_ReturnsCreatedAtActionResult()
        {
            var post = new Post { Title = "Test Title", Content = "Test Content", AuthorId = 1 };
            _mockPostService.Setup(service => service.CreatePostAsync(post)).ReturnsAsync(post);

            var result = await _controller.CreatePost(post);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task GetPost_ReturnsOkObjectResult_WhenPostExists()
        {
            var post = new Post { Id = 1, Title = "Test Title", Content = "Test Content", AuthorId = 1 };
            _mockPostService.Setup(service => service.GetPostByIdAsync(1, false)).ReturnsAsync(post);

            var result = await _controller.GetPost(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetPost_ReturnsNotFoundResult_WhenPostDoesNotExist()
        {
            _mockPostService.Setup(service => service.GetPostByIdAsync(1, false)).ReturnsAsync((Post)null);

            var result = await _controller.GetPost(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllPosts_ReturnsOkObjectResult()
        {
            var posts = new List<Post>
            {
                new Post { Id = 1, Title = "Test Title 1", Content = "Test Content 1", AuthorId = 1 },
                new Post { Id = 2, Title = "Test Title 2", Content = "Test Content 2", AuthorId = 2 }
            };
            _mockPostService.Setup(service => service.GetAllPostsAsync()).ReturnsAsync(posts);

            var result = await _controller.GetAllPosts();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
