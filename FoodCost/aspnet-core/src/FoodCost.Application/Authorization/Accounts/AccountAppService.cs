using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.ObjectMapping;
using Abp.Zero.Configuration;
using FoodCost.Authorization.Accounts.Dto;
using FoodCost.Authorization.Roles;
using FoodCost.Authorization.Users;
using FoodCost.Editions;
using FoodCost.MultiTenancy;
using FoodCost.MultiTenancy.Dto;

namespace FoodCost.Authorization.Accounts
{
    public class AccountAppService : FoodCostAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly TenantManager _tenantManager;
        private readonly EditionManager _editionManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IObjectMapper _objectMapper;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager,
            TenantManager tenantManager,
            EditionManager editionManager,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            UserManager userManager,
            RoleManager roleManager,
            IObjectMapper objectMapper)
        {
            _userRegistrationManager = userRegistrationManager;
            _tenantManager = tenantManager;
            _editionManager = editionManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _userManager = userManager;
            _roleManager = roleManager;
            _objectMapper = objectMapper;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var tenant = await CreateNewTenand(input);
            return new RegisterOutput
            {
                CanLogin = true
            };

            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }

        private async Task<TenantDto> CreateNewTenand(RegisterInput input)
        {
            try
            {
                // Create tenant
                var tenant = new Tenant
                {
                    IsActive = true,
                    TenancyName = NormalizeTenantName(input.Name, input.Surname, input.EmailAddress),
                    Name = input.Name + " " + input.Surname,

                };
                tenant.ConnectionString = null;

                var defaultEdition = await _editionManager.FindByNameAsync(EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    tenant.EditionId = defaultEdition.Id;
                }

                await _tenantManager.CreateAsync(tenant);
                await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

                // Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);

                // We are working entities of new tenant, so changing tenant filter
                using (CurrentUnitOfWork.SetTenantId(tenant.Id))
                {
                    // Create static roles for new tenant
                    CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                    await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                    // Grant all permissions to admin role
                    var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                    await _roleManager.GrantAllPermissionsAsync(adminRole);

                    // Create admin user for the tenant
                    var adminUser = User.CreateTenantUser(tenant.Id, input.UserName, input.Name, input.Surname, input.EmailAddress);
                    await _userManager.InitializeOptionsAsync(tenant.Id);
                    CheckErrors(await _userManager.CreateAsync(adminUser, input.Password));
                    await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                    // Assign admin user to role!
                    CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));
                    await CurrentUnitOfWork.SaveChangesAsync();
                }

                return _objectMapper.Map<TenantDto>(tenant);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw;
            }
        }

        private string NormalizeTenantName(string name, string surname, string email)
        {
            var mail = new MailAddress(email);
            email = mail.User;

            Regex rgx = new Regex("[^a-zA-Z0-9_-]");
            name = rgx.Replace(name, "");
            surname = rgx.Replace(surname, "");
            email = rgx.Replace(email, "");

            var tenantName = name;
            if (tenantName.Length < 7 && surname.Length > 0)
            {
                tenantName += "_" + surname;
            }
            if (tenantName.Length < 7 && email.Length > 0)
            {
                tenantName += "_" + email;
            }
            tenantName = tenantName.TrimStart('_');
            if (!char.IsLetter(tenantName.FirstOrDefault()))
            {
                tenantName = "fc" + tenantName;
            }

            var addRandom = 3;
            if (tenantName.Length < 10)
            {
                addRandom += 10 - tenantName.Length;
            }
            tenantName += "_" + Guid.NewGuid().ToString("N").Truncate(addRandom);

            return tenantName;
        }



    }
}
