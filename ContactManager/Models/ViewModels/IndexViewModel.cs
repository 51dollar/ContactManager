using ContactManager.Models.Entity;

namespace ContactManager.Models.ViewModels;

public class IndexViewModel
{
    public List<Contact> Contacts { get; set; } = new();
    public ContactViewModel Contact { get; set; } = new();
}