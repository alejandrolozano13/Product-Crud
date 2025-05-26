using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(string code);
        Task InsertAsync(Department department);
        Task UpdateAsync(Department department);
        Task RemoveAsync(string code);
    }
}