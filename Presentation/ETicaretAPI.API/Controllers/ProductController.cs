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
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly IOrderWriteRepository _orderWriteRepository;

        public ProductController(IProductReadRepository readRepository, IProductWriteRepository writeRepository, IOrderWriteRepository orderWriteRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]
        public async Task GetProducts()
        {
            Product p = await _readRepository.GetByIdAsync("16b8220e-fe28-4da9-96dc-beb87a1b0b7d");
            if(p != null)
            {
                p.Stock = p.Stock * 5;
            }
            _writeRepository.Update(p);
            await _writeRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getById(string id)
        {
            Product product = await _readRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
