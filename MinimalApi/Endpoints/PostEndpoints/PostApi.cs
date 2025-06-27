using Application.Posts.Commands;
using Application.Posts.Queries;
using Domain.Models;
using MediatR;

namespace MinimalApi.Endpoints.PostEndpoints
{
    public static class PostApi
    {
        public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            var v1 = app.MapGroup("api/v{version:apiVersion}").WithApiVersionSet().HasApiVersion(1, 0);

            v1.MapGet("/posts", async (IMediator mediator) =>
            {
                var getPosts = new GetAllPosts();
                var posts = await mediator.Send(getPosts);
                return Results.Ok(posts);
            })
                .WithName("GetAllPosts");

            v1.MapGet("/posts/{id}", async (IMediator mediator, int id) =>
            {
                var getPost = new GetPostById
                {
                    PostId = id
                };
                var post = await mediator.Send(getPost);
                if (post == null)
                {
                    return Results.NotFound(new { Message = "Post not found" });
                }
                return Results.Ok(post);
            })
               .WithName("GetPostById");

            v1.MapPost("/posts", async (IMediator mediator, Post post) =>
            {
                var createPost = new CreatePost
                {
                    PostContent = post.Content
                };
                var createdPost = await mediator.Send(createPost);
                return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
            })
                .WithName("CreatePost");

            v1.MapPut("/posts/{id}", async (IMediator mediator, Post post, int id) =>
            {
                var updatePost = new UpdatePost
                {
                    PostId = id,
                    PostContent = post.Content ?? string.Empty
                };
                var updatedPost = await mediator.Send(updatePost);
                if (updatedPost == null)
                {
                    return Results.NotFound(new { Message = "Post not found" });
                }
                return Results.Ok(updatedPost);
            })
                .WithName("UpdatePost");

            v1.MapDelete("/posts/{id}", async (IMediator mediator, int id) =>
            {
                var deletePost = new DeletePost
                {
                    PostId = id
                };
                var result = await mediator.Send(deletePost);
                if (!result)
                {
                    return Results.NotFound(new { Message = "Post not found" });
                }
                return Results.NoContent();
            })
                .WithName("DeletePost");

            return v1;
        }
    }
}
