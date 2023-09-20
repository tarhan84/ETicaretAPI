using ETicaretAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
    }
}
