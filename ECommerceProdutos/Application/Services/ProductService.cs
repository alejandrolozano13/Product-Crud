using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsycn();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsycn(id);
        }

        public async Task InsertAsync(Product product)
        {
            await _repository.InsertAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }
    }
}