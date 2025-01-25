using EventsCEC.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace EventsCEC.Domain.Repositories;

public interface IAuthenticateRepository
{
    Task<SignInResult> Authenticate(string email, string password);
    Task<AuthenticationProperties> ConfigureExternalAuthenticationProperties(string provider, string redirectUrl);
    Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
    Task SignInAsync(ApplicationUser applicationUser, bool isPersistent);
    Task Logout();
}
