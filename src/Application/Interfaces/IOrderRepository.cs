using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(string id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task CreateAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(string id);
}