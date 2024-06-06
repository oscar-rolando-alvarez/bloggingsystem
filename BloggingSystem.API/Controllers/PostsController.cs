using Microsoft.AspNetCore.Mvc;
using BloggingSystem.Application.Interfaces;
using BloggingSystem.Domain.Entities;
using System.Threading.Tasks;

namespace BloggingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post post)
        {
            var createdPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.Id }, createdPost);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id, [FromQuery] bool includeAuthor = false)
        {
            var post = await _postService.GetPostByIdAsync(id, includeAuthor);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }
    }
}
