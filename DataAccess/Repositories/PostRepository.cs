using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _context;
        public PostRepository(SocialDbContext context)
        {
            _context = context;
        }

        public async Task<Post?> CreatePost(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;
            post.UpdatedAt = DateTime.UtcNow;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return false;
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetPostById(int id)
        {
            return await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Post?> UpdatePost(int postId, Post post)
        {
            var postUpdated = await _context.Posts.FindAsync(postId);
            if (postUpdated == null)
            {
                return null;
            }
            postUpdated.Content = postUpdated.Content;
            postUpdated.UpdatedAt = DateTime.UtcNow;
            _context.Posts.Update(postUpdated);
            await _context.SaveChangesAsync();
            return postUpdated;
        }
    }
}
