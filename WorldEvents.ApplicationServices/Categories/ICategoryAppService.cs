using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorldEvents.Categories.Dto;

namespace WorldEvents.Categories
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, long, PagedAndSortedResultRequestDto>
    {

    }
}