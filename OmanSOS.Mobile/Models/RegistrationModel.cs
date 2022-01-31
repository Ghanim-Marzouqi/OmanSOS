using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Mobile.Models;

public class RegistrationModel
{
    [Required(ErrorMessage = "Please enter national ID")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "National ID is invalid")]
    public string NationalId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter email address")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter phone number")]
    [StringLength(8, ErrorMessage = "Phone number must be 8 digits")]
    [RegularExpression(@"^[79]\d{7}$", ErrorMessage = "Phone number is invalid")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter location")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords mismatch")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
