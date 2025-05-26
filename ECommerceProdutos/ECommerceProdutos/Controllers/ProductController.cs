using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProdutos.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados no E-Commerce
        /// </summary>
        /// <returns>Lista de produtos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        /// <summary>
        /// Retorna um produto pelo seu ID
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }

        /// <summary>
        /// Cadastra um novo produto no sistema.
        /// </summary>
        /// <param name="product">Objeto do produto a ser inserido</param>
        [HttpPost]
        public async Task<IActionResult> Add(Product product)
        {
            await _productService.InsertAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="product">Objeto do produto atualizado</param>
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            await _productService.UpdateAsync(product);
            return NoContent();
        }

        /// <summary>
        /// Remove um produto do sistema pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.RemoveAsync(id);
            return NoContent();
        }
    }
}
