using Models;

namespace EcoPower_Logistics.Repository
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        IEnumerable<Product> GetDepletedStock();
    }
}
