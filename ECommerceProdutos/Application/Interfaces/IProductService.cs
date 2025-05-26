using Domain.Entities;

namespace Application.Interfaces.ProductInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task InsertAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Guid id);
    }
}