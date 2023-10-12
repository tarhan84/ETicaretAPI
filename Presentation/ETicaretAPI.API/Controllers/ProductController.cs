using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Application.Models.Products;
using Microsoft.VisualBasic;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductReadRepository _readRepository;
        private readonly IProductWriteRepository _writeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductController(
            IProductReadRepository readRepository, 
            IProductWriteRepository writeRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var list = _readRepository.GetAll();
            list = list.OrderByDescending(item => item.CreateDateTime);
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

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource\\product-images");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            int index = 0;
            foreach(IFormFile file in Request.Form.Files)
            {
                DateTime now = DateTime.Now;
                string key = now.ToString($"yyyyMMddHHmmssfff{index}");
                string fullPath = Path.Combine(uploadPath,$"{key}_{file.FileName}");
                index++;

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

            }
            return Ok();
        }
    }
}
