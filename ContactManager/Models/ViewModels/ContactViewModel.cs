using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models.ViewModels;

public class ContactViewModel
{
    public Guid? Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "Minimum of 2 characters")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mobile Phone")]
    [Required(ErrorMessage = "Phone is required")]
    [RegularExpression(@"^\+?\d{11,15}$",
        ErrorMessage = "Phone must be in format +12345678901")]
    public string MobilePhone { get; set; } = String.Empty;

    [Display(Name = "Job Title")]
    [Required(ErrorMessage = "Job title is required")]
    [MinLength(2, ErrorMessage = "Minimum of 2 characters")]
    public string JobTitle { get; set; } = String.Empty;

    [Display(Name = "Birth Date")]
    [Required(ErrorMessage = "Birth date is required")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
}