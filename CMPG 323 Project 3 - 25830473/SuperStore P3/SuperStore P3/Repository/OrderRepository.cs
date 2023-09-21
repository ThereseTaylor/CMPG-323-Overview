using Data;
using Models;


namespace EcoPower_Logistics.Repository
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SuperStoreContext context) : base(context)
        {
        }

        //This method gets old orders, as they can possibly be deleted from the database.
        public IEnumerable<Order> GetOldOrders()
        {
            return _context.Orders.Where(o => o.OrderDate.Year < 2015).ToList();
        }
    }
}





