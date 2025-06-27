using Application.Abstractions;
using Application.Posts.Commands;
using MediatR;

namespace Application.Posts.CommandHandlers
{
    public class DeletePostHandler : IRequestHandler<DeletePost, bool>
    {
        private readonly IPostRepository _postRepo;
        public DeletePostHandler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public async Task<bool> Handle(DeletePost request, CancellationToken cancellationToken)
        {
            var post = await _postRepo.GetPostById(request.PostId);
            if (post == null)
            {
                return false;
            }
            return await _postRepo.DeletePost(request.PostId);
        }
    }
}
