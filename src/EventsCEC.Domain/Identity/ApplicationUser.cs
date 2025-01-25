using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public string Email { get; set; }

    public ApplicationUser GetUserDefault(string? name, string email)
        => new()
        {
            UserName = email,
            Name = name,
            Email = email,
            NormalizedUserName = email.ToUpper(),
            NormalizedEmail = email.ToUpper(),
            PhoneNumber = "11111111111",
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };
}
