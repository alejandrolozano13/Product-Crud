using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IValidator<Product> _validator;

        public ProductService(IProductRepository repository, IValidator<Product> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsycn();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new Exception("Deve informar um id");
            return await _repository.GetByIdAsycn(id);
        }

        public async Task InsertAsync(Product product)
        {
            if (product is null) throw new Exception("Deve informar um produto para inserção");

            var result = await _validator.ValidateAsync(product);
            if(!result.IsValid) throw new ValidationException(result.Errors);

            var produtoExistente = await _repository.GetByCodeAsync(product.Code);
            if (produtoExistente is not null) throw new Exception($"Já existe um produto com o código '{product.Code}'.");

            await _repository.InsertAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == Guid.Empty) throw new Exception("Deve informar um id");
            await _repository.RemoveAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            if (product is null) throw new Exception("Deve informar um produto para atualização");

            var result = await _validator.ValidateAsync(product);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _repository.UpdateAsync(product);
        }
    }
}