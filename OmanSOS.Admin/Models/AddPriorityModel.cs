using System.ComponentModel.DataAnnotations;

namespace OmanSOS.Admin.Models;

public class AddPriorityModel
{
    [Range(1, int.MaxValue, ErrorMessage = "Incorrect category id")]
    public int Id { get; set; } = 0;

    [Required(ErrorMessage = "Please enter priority")]
    public string Name { get; set; } = string.Empty;
}
