using Data;
using Models;
using System.Runtime.CompilerServices;

namespace EcoPower_Logistics.Repository
{
    public class CustomerRepository: GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SuperStoreContext context) : base(context)
        {}

        //This method can be used to get a list of all the customers who are male.
        public IEnumerable<Customer> GetMale()
        {
            return _context.Customers.Where(o => o.CustomerTitle == "Mr").ToList();
        }
    }
}


