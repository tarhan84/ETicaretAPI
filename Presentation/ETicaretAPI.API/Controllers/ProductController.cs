using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Application.Models.Products;

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
        public IActionResult GetProducts()
        {
            var list = _readRepository.GetAll();
            return (Ok(list));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            Product product = await _readRepository.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductModel productModel)
        {
            await _writeRepository.AddAsync(new()
            {
                Name = productModel.Name,
                Stock = productModel.Stock,
                Price = productModel.Price
            });
            await _writeRepository.SaveAsync();
            return Ok(productModel);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductModel productModel)
        {
            Product product = await _readRepository.GetByIdAsync(productModel.Id);
            product.Stock = productModel.Stock;
            product.Price = productModel.Price;
            product.Name = productModel.Name;
            await _writeRepository.SaveAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool deleted = _writeRepository.Remove(id);
            await _writeRepository.SaveAsync();
            return Ok(deleted);
        }
    }
}
