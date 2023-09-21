using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _readRepository;
        private readonly IProductWriteRepository _writeRepository;

        public ProductController(IProductReadRepository readRepository, IProductWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        [HttpGet]
        public async Task GetProducts()
        {
            await _writeRepository.AddRangeAsync(new()
            {
                new(){Id = Guid.NewGuid(), Name = "Test Product", Price = 65, Stock = 5}
            });
            _writeRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            Product product = await _readRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
