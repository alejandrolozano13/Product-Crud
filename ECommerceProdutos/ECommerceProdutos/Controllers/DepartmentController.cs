using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProdutos.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Retorna todos os departamentos cadastrados no E-Commerce
        /// </summary>
        /// <returns>Lista de departamentos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        /// <summary>
        /// Retorna um departamento pelo seu Código
        /// </summary>
        /// <param name="code">Código do departamento</param>
        /// <returns>Departamento encontrado</returns>
        [HttpGet("{code}")]
        public async Task<IActionResult> GetById(string code)
        {
            return Ok(await _departmentService.GetByIdAsync(code));
        }

        /// <summary>
        /// Cadastra um novo departamento no sistema.
        /// </summary>
        /// <param name="department">Objeto do departamento a ser inserido</param>
        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            await _departmentService.InsertAsync(department);
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um departamento existente.
        /// </summary>
        /// <param name="department">Objeto do departamento atualizado</param>
        [HttpPut]
        public async Task<IActionResult> Update(Department department)
        {
            await _departmentService.UpdateAsync(department);
            return NoContent();
        }

        /// <summary>
        /// Remove um departamento do sistema pelo Código.
        /// </summary>
        /// <param name="code">Código do departamento a ser removido</param>
        [HttpDelete("{code}")]
        public async Task<IActionResult> Remove(string code)
        {
            await _departmentService.RemoveAsync(code);
            return NoContent();
        }
    }
}