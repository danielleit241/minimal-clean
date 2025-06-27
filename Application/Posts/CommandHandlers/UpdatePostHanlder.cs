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
            var post = new Post
            {
                Content = request.PostContent,
            };
            return await _postRepo.UpdatePost(request.PostId, post);
        }
    }
}
