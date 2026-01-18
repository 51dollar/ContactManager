using ContactManager.Data.Repository;
using ContactManager.Models.Entity;
using ContactManager.Models.ViewModels;
using ContactManager.Service.Interfaces;

namespace ContactManager.Service;

public class ContactService(ContactRepository repository) : IContactService
{

    public async Task<List<Contact>> GetAllAsync() =>
        await repository.GetAllAsync();

    
    public async Task<Contact> AddAsync(ContactViewModel model)
    {
        var entity = new Contact
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            MobilePhone = model.MobilePhone,
            JobTitle = model.JobTitle,
            BirthDate = model.BirthDate!.Value
        };
        
        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();
        
        return entity;
    }

    public async Task<bool> UpdateAsync(Guid id, ContactViewModel model)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;
        
        entity.Name = model.Name;
        entity.MobilePhone = model.MobilePhone;
        entity.JobTitle = model.JobTitle;
        entity.BirthDate = model.BirthDate!.Value;
        
        await repository.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;
        
        repository.Delete(entity);
        await repository.SaveChangesAsync();
        
        return true;
    }
    
    private async Task<Contact?> GetByIdAsync(Guid id) => 
        await repository.GetByIdAsync(id);
}
