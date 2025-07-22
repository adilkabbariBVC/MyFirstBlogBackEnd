using MyFirstBlog.Entities;

namespace MyFirstBlog.Dtos;

public static class DtoExtensions
{
    public static PostDto AsDto(this Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Slug = post.Slug,
            Body = post.Body,
            CreatedDate = post.CreatedDate
        };
    }
}

