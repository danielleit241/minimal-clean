using Application.Abstractions;
using Application.Posts.Commands;
using Domain.Models;
using MediatR;

namespace Application.Posts.CommandHandlers
{
    public class UpdatePostHanlder : IRequestHandler<UpdatePost, Post?>
    {
        private readonly IPostRepository _postRepo;
        public UpdatePostHanlder(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<Post?> Handle(UpdatePost request, CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetPostById(request.PostId);
            if (post == null)
            {
                return null;
            }
            return await _postRepo.UpdatePost(request.PostId, post);
        }
    }
}
