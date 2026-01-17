using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models.ViewModels;

public class ContactViewModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Minimum 2 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression(@"^\+?\d{11,15}$",
        ErrorMessage = "Phone must be in format +12345678901")]

    public string MobilePhone { get; set; } = String.Empty;

    [Required(ErrorMessage = "Job title is required")]
    public string JobTitle { get; set; } = String.Empty;

    [Required(ErrorMessage = "Birth date is required")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }
}