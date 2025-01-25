using EventsCEC.Domain.Identity;
using EventsCEC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Infra.Data.Repositories;

public class SeedUserRoleInitialRepository : ISeedUserRoleInitialRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitialRepository(RoleManager<IdentityRole> roleManager,
          UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public void SeedUsers()
    {
        string password = "Numsey#2023";
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            var user = new ApplicationUser()
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                Name = "Usuario",
                PhoneNumber = "11111111111",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "User").Wait();
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            var user = new ApplicationUser()
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                Name = "Admin",
                PhoneNumber = "11111111111",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = _userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
                _userManager.AddToRoleAsync(user, "Admin").Wait();
        }
    }

    public async void SeedRoles()
    {
        var existUser = await _roleManager.RoleExistsAsync("User");
        var existAdmin = await _roleManager.RoleExistsAsync("Admin");

        if (!existUser)
            await _roleManager.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "USER" });

        if (!existAdmin)
            await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" });
    }
}
