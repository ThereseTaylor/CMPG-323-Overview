using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class CustomerRepository
    {
        protected readonly SuperStoreContext _context = new SuperStoreContext();

        // GET ALL: Customers
        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        // TO DO: Add ‘Get By Id’
        public Customer GetCustomerByID(int id)
        {
            return _context.Customers.Find(id);
        }

        // TO DO: Add ‘Create’
        public void InsertCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        // TO DO: Add ‘Edit’
        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

        }

        // TO DO: Add ‘Delete’
        public void DeleteCustomer(int customerID)
        {
            Customer customer = _context.Customers.Find(customerID);
            _context.Customers.Remove(customer);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        // TO DO: Add ‘Exists’
        public bool Exists(int customerID)
        {
            return (_context.Customers?.Any(e => e.CustomerId == customerID)).GetValueOrDefault();
        }
    }
}
