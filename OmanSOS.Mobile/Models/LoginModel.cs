using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Mobile.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Please enter email address")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter password")]
    public string Password { get; set; } = string.Empty;
}
