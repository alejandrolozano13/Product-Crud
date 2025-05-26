using Domain.Entities;
using Domain.IRepositories;

namespace Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<IEnumerable<Product>> GetAllAsycn()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsycn(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}