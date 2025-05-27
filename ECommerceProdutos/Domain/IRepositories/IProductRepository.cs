using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsycn();
        Task<Product> GetByIdAsycn(Guid id);
        Task<Product> GetByCodeAsync(string code);
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}