using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorldEvents.Users.Dto;

namespace WorldEvents.Users
{
    public interface IUserAppService : IApplicationService
    {
        Task ProhibitPermission(ProhibitPermissionInput input);

        Task RemoveFromRole(long userId, string roleName);

        Task<ListResultDto<UserListDto>> GetUsers();

        Task CreateUser(CreateUserInput input);
    }
}