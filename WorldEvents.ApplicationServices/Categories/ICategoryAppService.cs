using Abp.Application.Services;
using Abp.Application.Services.Dto;

namespace WorldEvents.Categories
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto, long, PagedAndSortedResultRequestDto>
    {

    }
}