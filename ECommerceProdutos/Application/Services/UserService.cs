using Application.Interfaces;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<User> _validator;

        public UserService(IUserRepository userRepository, IValidator<User> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task Add(User user)
        {
            var result = await _validator.ValidateAsync(user);
            if(!result.IsValid) throw new ValidationException(result.Errors);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Add(user);
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty) throw new Exception("Deve informar um id.");
            await _userRepository.Delete(id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<User> GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new Exception("Deve informar um e-mail");
            return await _userRepository.GetByEmail(email);
        }

        public async Task<User> GetById(Guid id)
        {
            if (id == Guid.Empty) throw new Exception("Deve informar um id.");
            return await _userRepository.GetById(id);
        }

        public async Task Update(Guid id, User user)
        {
            if (id == Guid.Empty) throw new Exception("Deve informar um id.");
            if (user is null) throw new Exception("Deve informar os dados do usuário para atualizar");

            var result = await _validator.ValidateAsync(user);
            if(!result.IsValid) throw new ValidationException(result.Errors);

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.Update(id, user);
        }
    }
}