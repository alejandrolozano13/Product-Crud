using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProdutos.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados no E-Commerce
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        /// <summary>
        /// Cadastra um novo usuário no sistema.
        /// </summary>
        /// <param name="user">Objeto do usuário a ser inserido</param>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            await _userService.Add(user);
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="user">Objeto do usuário atualizado</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] User user)
        {
            await _userService.Update(id, user);
            return NoContent();
        }

        /// <summary>
        /// Remove um usuário do sistema pelo Id.
        /// </summary>
        /// <param name="id">Id do usuário a ser removido</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _userService.Delete(id);
            return NoContent();
        }
    }
}