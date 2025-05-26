using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task Add(User user);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
    }
}