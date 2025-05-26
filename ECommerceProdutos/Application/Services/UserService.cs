using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(User user)
        {
            await _userRepository.Add(user);
        }

        public async Task Delete(string id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<User> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task Update(string id, User user)
        {
            await _userRepository.Update(id, user);
        }
    }
}