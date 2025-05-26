using Domain.Entities;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(string code);
        Task InsertAsync(Department department);
        Task UpdateAsync(Department department);
        Task RemoveAsync(string code);
    }
}