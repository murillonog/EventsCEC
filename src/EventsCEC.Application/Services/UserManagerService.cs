using AutoMapper;
using EventsCEC.Application.Dtos;
using EventsCEC.Application.Interfaces;
using EventsCEC.Domain.Identity;
using EventsCEC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EventsCEC.Application.Services;

public class UserManagerService : IUserManagerService
{
    private readonly IMapper _mapper;
    private readonly IUserManagerRepository _userManagerRepository;

    public UserManagerService(IMapper mapper, IUserManagerRepository userManagerRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManagerRepository = userManagerRepository ?? throw new ArgumentNullException(nameof(userManagerRepository));
    }

    public async Task<IdentityResult> CreateUserAsync(string name, string email, string provider, string providerKey)
    {
        var user = new ApplicationUser().GetUserDefault(name, email);

        var userLoginInfo = new UserLoginInfo(provider, providerKey, provider);

        return await _userManagerRepository.CreateUserAsync(user, "Default#123", userLoginInfo);
    }

    public async Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto, string password)
        => await _userManagerRepository.RegisterUser(_mapper.Map<ApplicationUser>(userRegisterDto), password);

    public async Task<UserRegisterDto?> FindByLoginAsync(string provider, string providerKey)
    {
        var result = await _userManagerRepository.FindByLoginAsync(provider, providerKey);
        return _mapper.Map<UserRegisterDto>(result);
    }
}
