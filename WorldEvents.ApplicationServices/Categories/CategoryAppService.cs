using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using WorldEvents.Categories.Dto;
using WorldEvents.Entities; 

namespace WorldEvents.Categories
{
    public class CategoryAppService : AsyncCrudAppService<Category, CategoryDto, long, PagedAndSortedResultRequestDto>, ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, long> repository)
            : base(repository)
        {

        }
    }
}
