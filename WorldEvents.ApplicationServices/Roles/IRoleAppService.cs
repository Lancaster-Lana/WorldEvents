using System.Threading.Tasks;
using Abp.Application.Services;
using WorldEvents.Roles.Dto;

namespace WorldEvents.Roles
{
    public interface IRoleAppService : IApplicationService
    {
        Task UpdateRolePermissions(UpdateRolePermissionsInput input);
    }
}
