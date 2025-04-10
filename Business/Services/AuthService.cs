using Business.Models;
using Data.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService(IUserService userService, SignInManager<UserEntity> signInManager)
{
    private readonly IUserService _userService = userService;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;

    public async Task<SignInResult> SignInAsync(SignInFormData formData)
    {
        if (formData == null)
            return new AuthResult { Succeded = false, StatusCode = 400, Error = "Not all required field are supplied." };

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersisent, false);
        return result.Succeeded
            ? new AuthResult { Succeded = true, StatusCode = 201 }
            : new AuthResult { Succeded = false, StatusCode = 401, Error = "Invalid email or password." };
    }

    public async Task<AuthResult> SignUpAsync(SignUpFormData formData)
    {
        if (formData == null)
            return new AuthResult { Succeded = false, StatusCode = 400, Error = "Not all required field are supplied." };

        var result = await _userService.CreateUserAsync(formData);
        return result.Succeded
            ? new AuthResult { Succeded = true, StatusCode = 201 }
            : new AuthResult { Succeded = false, StatusCode = result.StatusCode, Error = result.Error };
    }

    public async Task<AuthResult> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return new AuthResult { Succeded = true, StatusCode = 200 };
    }
}
