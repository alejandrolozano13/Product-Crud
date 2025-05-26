using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateUser(LoginDto loginDto);
    }
}