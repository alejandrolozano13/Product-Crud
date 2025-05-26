using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;
        private readonly IValidator<Department> _validator;

        public DepartmentService(IDepartmentRepository repository, IValidator<Department> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(string code)
        {
            if (string.IsNullOrEmpty(code)) throw new Exception("Deve informar um código");
            return await _repository.GetByIdAsync(code);
        }

        public async Task InsertAsync(Department department)
        {
            if (department is null) throw new Exception("Deve informar um departamento para inserção");

            var result = await _validator.ValidateAsync(department);
            if(!result.IsValid) throw new ValidationException(result.Errors);

            await _repository.InsertAsync(department);
        }

        public async Task RemoveAsync(string code)
        {
            if(string.IsNullOrEmpty(code)) throw new Exception("Deve informar um código");
            await _repository.RemoveAsync(code);
        }

        public async Task UpdateAsync(Department department)
        {
            if (department is null) throw new Exception("Deve informar um departamento para atualização");

            var result = await _validator.ValidateAsync(department);
            if (!result.IsValid) throw new ValidationException(result.Errors);

            await _repository.UpdateAsync(department);
        }
    }
}