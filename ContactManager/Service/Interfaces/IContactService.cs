using ContactManager.Models.Entity;
using ContactManager.Models.ViewModels;

namespace ContactManager.Service.Interfaces;

public interface IContactService
{
    public Task<List<Contact>> GetAllAsync();
    public Task<Contact> AddAsync(ContactViewModel model);
    public Task<bool> UpdateAsync(Guid id, ContactViewModel model);
    public Task<bool> DeleteAsync(Guid id);
    public Task<int> DeleteRangeAsync(IEnumerable<Guid> ids);
}