using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsycn();
        Task<Product> GetByIdAsycn(Guid id);
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}