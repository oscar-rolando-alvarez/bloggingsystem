using BloggingSystem.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BloggingSystem.Application.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);
        Task<Post> GetPostByIdAsync(int id, bool includeAuthor = false);
        Task<IEnumerable<Post>> GetAllPostsAsync();
    }
}
