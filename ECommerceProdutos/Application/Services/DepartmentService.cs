using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(string code)
        {
            return await _repository.GetByIdAsync(code);
        }

        public async Task InsertAsync(Department department)
        {
            await _repository.InsertAsync(department);
        }

        public async Task RemoveAsync(string code)
        {
            await _repository.RemoveAsync(code);
        }

        public async Task UpdateAsync(Department department)
        {
            await _repository.UpdateAsync(department);
        }
    }
}