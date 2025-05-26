using Domain.Entities;
using Domain.IRepositories;

namespace Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task Add(User usuario)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailTaken(string email)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}