using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
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