using System.ComponentModel.DataAnnotations;

namespace EventsCEC.Application.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Passwords don´t match")]
    public string ConfirmPassword { get; set; }
    [Required, MaxLength(255)]
    public string Name { get; set; }
    [Required]
    [Phone]
    public string Phone { get; set; }
}
