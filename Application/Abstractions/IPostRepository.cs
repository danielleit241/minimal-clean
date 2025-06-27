using Domain.Models;

namespace Application.Abstractions
{
    public interface IPostRepository
    {
        Task<ICollection<Post>> GetAllPosts();
        Task<Post?> GetPostById(int id);
        Task<Post?> CreatePost(Post post);
        Task<Post?> UpdatePost(int postId, Post post);
        Task<bool> DeletePost(int id);
    }
}
