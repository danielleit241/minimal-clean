using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.Abstractions;

namespace MinimalApi.EndpointDefinitions
{
    public class PostEndpointsDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var v1 = app.MapGroup("api/v{version:apiVersion}/posts").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("", GetAllPosts)
                .WithName("GetAllPosts");

            v1.MapGet("/{id}", GetPostById)
               .WithName("GetPostById");

            v1.MapPost("", CreatePost)
                .WithName("CreatePost");

            v1.MapPut("/{id}", UpdatePost)
                .WithName("UpdatePost");

            v1.MapDelete("/{id}", DeletePost)
                .WithName("DeletePost");
        }

        private async Task<Results<Ok<ICollection<Post>>, NotFound>> GetAllPosts(IMediator mediator)
        {
            var getPosts = new GetAllPosts();
            var posts = await mediator.Send(getPosts);
            if (posts == null || !posts.Any())
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(posts);
        }

        private async Task<Results<Ok<Post>, NotFound>> GetPostById(IMediator mediator, int id)
        {
            var getPost = new GetPostById
            {
                PostId = id
            };
            var post = await mediator.Send(getPost);
            if (post == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(post);
        }

        private async Task<CreatedAtRoute<Post>> CreatePost(IMediator mediator, Post post)
        {
            var createPost = new CreatePost
            {
                PostContent = post.Content
            };
            var createdPost = await mediator.Send(createPost);
            return TypedResults.CreatedAtRoute(createdPost, "GetPostById", new { id = createdPost.Id });
        }

        private async Task<Results<Ok<Post>, NotFound>> UpdatePost(IMediator mediator, Post post, int id)
        {
            var updatePost = new UpdatePost
            {
                PostId = id,
                PostContent = post.Content ?? string.Empty
            };
            var updatedPost = await mediator.Send(updatePost);
            if (updatedPost == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(updatedPost);
        }

        private async Task<Results<NoContent, NotFound>> DeletePost(IMediator mediator, int id)
        {
            var deletePost = new DeletePost
            {
                PostId = id
            };
            var result = await mediator.Send(deletePost);
            if (!result)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.NoContent();
        }
    }
}
