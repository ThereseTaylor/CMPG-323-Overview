using Data;
using EcoPower_Logistics.Repository;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository()
        {
        }

        public IEnumerable<Product> GetDepletedStock()
        {
            return _context.Products.Where(p => p.UnitsInStock == 0).ToList();
        }
    }
}

