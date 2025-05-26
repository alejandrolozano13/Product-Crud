using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProdutos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Autentica um usuário no sistema e retorna um token JWT.
        /// </summary>
        /// <param name="loginDto">Credenciais do usuário (e-mail e senha).</param>
        /// <returns>
        /// Um objeto contendo o token JWT e os dados do usuário autenticado.
        /// Retorna 200 OK com o token em caso de sucesso ou 401 Unauthorized se as credenciais forem inválidas.
        /// </returns>
        /// <response code="200">Autenticação bem-sucedida. Retorna o token JWT e os dados do usuário.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var userDto = await _authService.AuthenticateUser(loginDto);
            return Ok(new
            {
                Token = userDto.Token,
                User = userDto.User
            });
        }
    }
}
