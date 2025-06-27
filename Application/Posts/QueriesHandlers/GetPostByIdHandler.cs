using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;

namespace Application.Posts.QueriesHandlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, Post?>
    {
        private readonly IPostRepository _postRepo;
        public GetPostByIdHandler(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }
        public async Task<Post?> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            return await _postRepo.GetPostById(request.PostId);
        }
    }
}
