using ContactManager.Data.Repository;
using ContactManager.Models.Entity;
using ContactManager.Service.Interfaces;

namespace ContactManager.Service;

public class ContactService(ContactRepository repository) : IContactService
{
    public async Task<List<Contact>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Contact?> GetByIdAsync(Guid id)
    {
        return await repository.GetByIdAsync(id);
    }
    
    public async Task AddAsync(Contact contact)
    {
        await repository.AddAsync(contact);
        await repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, Contact contact)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return;
        
        entity.Name = contact.Name;
        entity.MobilePhone = contact.MobilePhone;
        entity.JobTitle = contact.JobTitle;
        entity.BirthDate = contact.BirthDate;
        
        await repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return;
        
        repository.Delete(entity);
        await repository.SaveChangesAsync();
    }

}