using Domain.Entities;

namespace Application.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(string id);
    Task<IEnumerable<Review>> GetAllAsync();
    Task CreateAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Review review);
}