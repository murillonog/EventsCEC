using EventsCEC.Domain.Identity;
using EventsCEC.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Infra.Data.Repositories;

public class AuthenticateRepository : IAuthenticateRepository
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthenticateRepository(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task Logout()
        => await _signInManager.SignOutAsync();

    public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        => await _signInManager.GetExternalAuthenticationSchemesAsync();

    public async Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl)
        => await Task.Run(() => _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl));

    public async Task<SignInResult> Authenticate(string email, string password)
        => await _signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

    public async Task SignInAsync(ApplicationUser applicationUser, bool isPersistent)
        => await _signInManager.SignInAsync(applicationUser, isPersistent: isPersistent);
}
