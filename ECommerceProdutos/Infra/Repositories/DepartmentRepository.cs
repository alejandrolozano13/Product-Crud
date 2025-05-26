using Domain.Entities;
using Domain.IRepositories;

namespace Infra.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetByIdAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}