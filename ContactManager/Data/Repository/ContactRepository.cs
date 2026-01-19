using ContactManager.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repository;

public class ContactRepository(AppDbContext context) : BaseRepository<Contact>(context)
{
    private readonly AppDbContext _context = context;

    public virtual async Task<Contact?> GetByIdAsync(Guid id) =>
        await _context.Set<Contact>().FindAsync(id);
    
    public Task<int> DeleteByIdsAsync(IEnumerable<Guid> ids) =>
        _context.Contacts
            .Where(x => ids.Contains(x.Id))
            .ExecuteDeleteAsync();
}