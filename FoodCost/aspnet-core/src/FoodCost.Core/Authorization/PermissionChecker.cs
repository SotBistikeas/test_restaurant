using Abp.Authorization;
using FoodCost.Authorization.Roles;
using FoodCost.Authorization.Users;

namespace FoodCost.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
