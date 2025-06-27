using Application.Abstractions;
using Application.Posts.Commands;
using Asp.Versioning;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Booststrapping
{
    public static class ApplicationServiceExtensions
    {
        public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<SocialDbContext>(
                opt => opt.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    )
                );
            builder.Services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = ApiVersionReader.Combine(
                        new UrlSegmentApiVersionReader(),
                        new HeaderApiVersionReader("X-Version")
                        );
                });
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(typeof(CreatePost));
            return builder;
        }
    }
}
