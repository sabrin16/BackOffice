using System.ComponentModel.DataAnnotations;

namespace Presention.Models;

public class SignInViewModel
{
    [Required]
    [Display(Name = "Email", Prompt = "Enter email address")]
    [DataType(DataType.EmailAddress)]

    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Password", Prompt = "Enter password")]
    [DataType(DataType.Password)]

    public string Password { get; set; } = null!;

    public bool IsPersistent { get; set; }
}
