using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorldEvents.MultiTenancy.Dto;

namespace WorldEvents.MultiTenancy
{
    public interface ITenantAppService : IApplicationService
    {
        ListResultDto<TenantListDto> GetTenants();

        Task CreateTenant(CreateTenantInput input);
    }
}
