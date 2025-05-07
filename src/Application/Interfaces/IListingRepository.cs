using Domain.Entities;

namespace Application.Interfaces;

public interface IListingRepository
{
    Task<Listing?> GetByIdAsync(string id);
    Task<IEnumerable<Listing>> GetAllAsync();
    Task CreateAsync(Listing listing);
    Task UpdateAsync(Listing listing);
    Task DeleteAsync(string id);
}
