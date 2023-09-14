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
        CustomerRepository customerRepository = new CustomerRepository();

        // GET: Customers
        public async Task<IActionResult> Index()
        {

            var results = customerRepository.GetAll();

            return View(results);
        }


        // GET: Customers/Details/5
        public ViewResult Details(int id)
        {
            Customer customer = customerRepository.GetCustomerByID(id);
            return View(customer);
        }


        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.InsertCustomer(customer);
                customerRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = customerRepository.GetCustomerByID(id);
            return View(customer);
        }

        //POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, CellPhone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.UpdateCustomer(customer);
                customerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(customer);
        }


        // GET: Customers/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Customer customer = customerRepository.GetCustomerByID(id);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (CustomerExists(id))
            {
                Customer customer = customerRepository.GetCustomerByID(id);
                customerRepository.DeleteCustomer(id);
                customerRepository.Save();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return customerRepository.Exists(id);
        }
    }
}
