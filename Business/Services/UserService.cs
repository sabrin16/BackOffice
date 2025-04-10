using System.Diagnostics;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public interface IUserService
{
    Task<UserResult> AddUserToRole(string userId, string roleName);
    Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User");
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

    public async Task<UserResult> CreateUserAsync(SignUpFormData formData, string roleName = "User")
    {
        if (formData == null)
            return new UserResult { Succeded = false, StatusCode = 400, Error = "form data can´t be null." };

        var existsResult = await _userRepository.ExistsAsync(x => x.Email == formData.Email);
        if (existsResult.Succeeded)
            return new UserResult { Succeded = false, StatusCode = 409, Error = "user with same email already exists." };
    
        try
        {
            var userEntity = formData.MapTo<UserEntity>();

            var result = await _userManager.CreateAsync(userEntity, formData.Password);
            if (result.Succeeded)
            {
                var addToRoleResult = await AddUserToRole(userEntity.Id, roleName);
                return result.Succeeded
                    ? new UserResult { Succeded = true, StatusCode = 201 }
                    : new UserResult { Succeded = false, StatusCode = 201, Error = "User created but not added to role." };

            }
                   
                   return new UserResult { Succeded = false, StatusCode = 500, Error = "Unable to add create user." };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new UserResult { Succeded = false, StatusCode = 500, Error = ex.Message };
        }
    }
}
