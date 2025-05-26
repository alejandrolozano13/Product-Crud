using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProdutos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetById(string code)
        {
            return Ok(await _departmentService.GetByIdAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            await _departmentService.InsertAsync(department);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Department department)
        {
            await _departmentService.UpdateAsync(department);
            return NoContent();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Remove(string code)
        {
            await _departmentService.RemoveAsync(code);
            return NoContent();
        }
    }
}