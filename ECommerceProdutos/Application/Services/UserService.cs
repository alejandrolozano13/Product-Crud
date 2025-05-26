using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        public Task Add(User user)
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

        public Task Update(string id, User user)
        {
            throw new NotImplementedException();
        }
    }
}