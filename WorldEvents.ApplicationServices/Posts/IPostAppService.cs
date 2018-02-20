using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Samples.Blog.Posts.Dtos;

namespace WorldEvents.Posts
{
    public interface IPostAppService : IApplicationService
    {
        Task<PagedResultOutput<PostDto>> GetPosts(GetPostsInput input);

        Task CreatePost(CreatePostInput input);
    }
}