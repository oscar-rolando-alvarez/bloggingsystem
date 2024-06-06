using BloggingSystem.Application.Interfaces;
using BloggingSystem.Domain.Entities;
using BloggingSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggingSystem.Application.Services
{
    public class PostService : IPostService
    {
        private readonly BloggingContext _context;

        public PostService(BloggingContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> GetPostByIdAsync(int id, bool includeAuthor = false)
        {
            if (includeAuthor)
            {
                return await _context.Posts.Include(p => p.Author).FirstOrDefaultAsync(p => p.Id == id);
            }
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Author).ToListAsync();
        }
    }
}
