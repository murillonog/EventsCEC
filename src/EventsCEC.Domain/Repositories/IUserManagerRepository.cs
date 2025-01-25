using EventsCEC.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Domain.Repositories;

public interface IUserManagerRepository
{
    Task<bool> RegisterUser(ApplicationUser applicationUser, string password);
    Task<ApplicationUser?> FindByLoginAsync(string provider, string providerKey);
    Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password, UserLoginInfo userLoginInfo);
}
