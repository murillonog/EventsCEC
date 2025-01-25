using EventsCEC.Domain.Identity;
using EventsCEC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Infra.Data.Repositories;

public class UserManagerRepository : IUserManagerRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserManagerRepository(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<ApplicationUser?> FindByLoginAsync(string provider, string providerKey)
        => await _userManager.FindByLoginAsync(provider, providerKey);

    public async Task<bool> RegisterUser(ApplicationUser applicationUser, string password)
    {
        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
            await _signInManager.SignInAsync(applicationUser, isPersistent: false);

        return result.Succeeded;
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password, UserLoginInfo userLoginInfo)
    {
        await _userManager.CreateAsync(applicationUser, password);
        var result = await _userManager.AddLoginAsync(applicationUser, userLoginInfo);

        if (result.Succeeded)
            await _signInManager.SignInAsync(applicationUser, isPersistent: false);

        return result;
    }
}
