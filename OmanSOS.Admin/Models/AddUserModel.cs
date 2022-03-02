using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Admin.Models;

public class AddUserModel
{
    [Required(ErrorMessage = "Please enter user's national ID")]
    public int NationalId { get; set; }

    [Required(ErrorMessage = "Please enter user's Name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter user's email address")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter user's phone number")]
    [StringLength(8, ErrorMessage = "Phone number must be 8 digits")]
    [RegularExpression(@"^[79]\d{7}$", ErrorMessage = "Phone number is invalid")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select user type")]
    public int UserTypeId { get; set; } = 1;

    [Required(ErrorMessage = "Please choose location")]
    public string Location { get; set; } = string.Empty;
}
