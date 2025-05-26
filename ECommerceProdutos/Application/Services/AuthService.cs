using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Application.DTOs;
using Application.Interfaces;
using Domain.IRepositories;
using Domain.Utils;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> AuthenticateUser(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmail(loginDto.UserAPI);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new UnauthorizedAccessException("Credenciais inválidas");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnviromentsVariables.Jwt_Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new System.Security.Claims.Claim("email", user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: EnviromentsVariables.Jwt_Issuer,
                audience: EnviromentsVariables.Jwt_Audience,
                claims: payload,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserDto
            {
                User = user.Email,
                Token = tokenString
            };
        }
    }
}