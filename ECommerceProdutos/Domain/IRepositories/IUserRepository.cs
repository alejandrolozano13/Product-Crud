using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task Add(User usuario);
        Task Update(Guid id, User user);
        Task Delete(Guid id);
        Task<bool> IsEmailTaken(string email);
    }
}