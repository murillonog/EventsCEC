using AutoMapper;
using EventsCEC.Application.Dtos;
using EventsCEC.Application.Interfaces;
using EventsCEC.Application.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventsCEC.App.Controllers;
public class AuthenticationController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAuthenticateService _authenticationService;
    private readonly IUserManagerService _userManagerService;

    public AuthenticationController(IMapper mapper,
        IAuthenticateService authenticationService,
        IUserManagerService userManagerService)
    {
        _mapper = mapper;
        _authenticationService = authenticationService;
        _userManagerService = userManagerService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl)
    {
        var providers = await _authenticationService.GetExternalAuthenticationSchemesAsync();
        var login = new LoginViewModel { ReturnUrl = returnUrl, Providers = providers };
        return View(login);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        var result = await _authenticationService.Authenticate(loginViewModel.Email, loginViewModel.Password);

        if (result.Succeeded)
        {
            if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(loginViewModel.ReturnUrl);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong).");
            return View(loginViewModel);
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        var userRegister = _mapper.Map<UserRegisterDto>(registerViewModel);
        var result = await _userManagerService.RegisterUserAsync(userRegister, registerViewModel.Password);

        if (result)
            return Redirect("/");

        ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong.");

        return View(registerViewModel);
    }
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await _authenticationService.Logout();
        return Redirect("/Authentication/Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExternalLogin(string provider, string? returnUrl)
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl, provider });
        var properties = await _authenticationService.ConfigureExternalAuthenticationProperties(provider, redirectUrl!);
        return Challenge(properties, provider);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalLoginCallback(string provider, string? returnUrl)
    {
        var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);

        if (!result.Succeeded)
            return RedirectToAction(nameof(Login));

        var userIdClaim = result.Principal.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return RedirectToAction("Index", "Home");

        var user = await _userManagerService.FindByLoginAsync(provider, userIdClaim.Value);
        if (user is null)
        {
            var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = result.Principal.FindFirst(ClaimTypes.Name)?.Value;
            await _userManagerService.CreateUserAsync(name, email, provider, userIdClaim.Value);
        }
        else
        {
            await _authenticationService.SignInAsync(user, false);
        }

        return RedirectToAction("Index", "Home");
    }
}
