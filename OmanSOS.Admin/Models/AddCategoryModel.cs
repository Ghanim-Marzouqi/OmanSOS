using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Admin.Models;

public class AddCategoryModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Incorrect category id")]
    public int Id { get; set; } = 0;

    [Required(ErrorMessage = "Please enter category")]
    public string Name { get; set; } = string.Empty;

    public bool IsEmergency { get; set; }
}
