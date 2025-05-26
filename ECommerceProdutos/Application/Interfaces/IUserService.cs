using Domain.Entities;

namespace Application.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByEmail(string email);
        Task Add(User user);
        Task Update(string id, User user);
        Task Delete(string id);
    }
}