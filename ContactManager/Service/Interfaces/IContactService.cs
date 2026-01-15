using ContactManager.Models.Entity;

namespace ContactManager.Service.Interfaces;

public interface IContactService
{
    public Task<List<Contact>> GetAllAsync();
    public Task<Contact?> GetByIdAsync(Guid id);
    public Task AddAsync(Contact contact);
    public Task UpdateAsync(Guid id, Contact contact);
    public Task DeleteAsync(Guid id);
}