using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using EcoPower_Logistics.Repository;

namespace Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private IGenericRepository<Customer> genericRepository = null;
        private ICustomerRepository customerRepository;

        public CustomersController(IGenericRepository<Customer> repository, ICustomerRepository customerRepository)
        {
            this.genericRepository = repository;
            this.customerRepository = customerRepository;
        }

        // GET: Customers
        public ActionResult Index()
        {
            var results = genericRepository.GetAll().ToList();
            return View(results);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || genericRepository.GetAll() == null)
            {
                return NotFound();
            }

            var result = genericRepository.GetAll().FirstOrDefault(c => c.CustomerId == id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        //GET: Customers/MaleCustomers
        public ActionResult MaleCustomers()
        {

            if (customerRepository.GetMale() == null)
            {
                return NotFound();
            }

            var result = customerRepository?.GetMale();
            return View(result);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var lastId = genericRepository.GetAll().OrderBy(i => i.CustomerId).LastOrDefault().CustomerId;
            List<int> newList = new List<int>();
            newList.Add(lastId + 1);
            ViewData["CustomerId"] = new SelectList(newList);
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Insert(customer);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            var lastId = genericRepository.GetAll().OrderBy(i => i.CustomerId).LastOrDefault().CustomerId;
            List<int> newList = new List<int>();
            newList.Add(lastId + 1);
            ViewData["CustomerId"] = new SelectList(newList, customer.CustomerId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Customer customer = genericRepository.GetById(id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {             
                try
                {
                    genericRepository.Update(customer);
                    genericRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(customer);
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Customer customer = genericRepository.GetById(id);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return Problem("Entity set 'SuperStoreContext.Customer'  is null.");
            }

            var customer = genericRepository.GetById(id);

            if (customer != null)
            {
                genericRepository.Delete(id);
            }

            genericRepository.Save();
            return RedirectToAction(nameof(Index));
         
        }

        private bool CustomerExists(int id)
        {
            return (genericRepository.GetAll()?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
