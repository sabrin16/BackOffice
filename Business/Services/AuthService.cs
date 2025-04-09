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
            return new SignInResult();

        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, formData.IsPersisent, false);
        return result;
    }

    public async Task SignUpAsync()
}
