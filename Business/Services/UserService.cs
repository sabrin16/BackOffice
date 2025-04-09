using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<UserResult> AddUserToRole(string userId, string roleName);
    Task<UserResult> GetUserAsync();
}

public class UserService(IUserRepository userRepository, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IUserService
{
    private readonly IUserRepository _UserRepository = userRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<UserResult> GetUserAsync()
    {
        var result = await _UserRepository.GetAllAsync();
        return result.MapTo<UserResult>();
    }


    public async Task<UserResult> AddUserToRole(string userId, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            return new UserResult { Succeded = false, StatusCode = 404, Error = "Role dosn´t exists." };

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new UserResult { Succeded = false, StatusCode = 404, Error = "User dosn´t exists." };

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded
            ? new UserResult { Succeded = true, StatusCode = 200 }
            : new UserResult { Succeded = false, StatusCode = 500, Error = "Unable to add user to role." };
    }
}
