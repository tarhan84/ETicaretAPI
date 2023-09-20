using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Concretes
{
    
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new()
            {
                new() {Id = Guid.NewGuid(), Name = "Product 1", Price = 456, Stock = 50},
                new() {Id = Guid.NewGuid(), Name = "Product 2", Price = 100, Stock = 100},
                new() {Id = Guid.NewGuid(), Name = "Product 3", Price = 45, Stock = 2000},
                new() {Id = Guid.NewGuid(), Name = "Product 4", Price = 250, Stock = 120},
                new() {Id = Guid.NewGuid(), Name = "Product 5", Price = 120, Stock = 70},
            };
    }
}
