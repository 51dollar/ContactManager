
namespace ContactManager.Models.Entity;

public class Contact
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string MobilePhone { get; set; }
    public required string JobTitle { get; set; }
    public required DateTime BirthDate { get; set; }
}