using MediatR;

namespace Application.Posts.Commands
{
    public class DeletePost : IRequest<bool>
    {
        public int PostId { get; set; }
    }
}
