using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FoodCost.Roles.Dto;
using FoodCost.Users.Dto;

namespace FoodCost.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
