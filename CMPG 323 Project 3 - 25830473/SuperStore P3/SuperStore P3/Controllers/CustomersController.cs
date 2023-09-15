﻿using System;
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

        public CustomersController(IGenericRepository<Customer> repository)
        {
            this.genericRepository = repository;
        }

        // GET: Customers
        public ActionResult Index()
        {
            var results = genericRepository.GetAll();
            return View(results);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, Cellphone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Insert(customer);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = genericRepository.GetById(id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("CustomerId, CustomerTitle, CustomerName, CustomerSurname, Cellphone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Update(customer);
                genericRepository.Save();
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
            genericRepository.Delete(id);
            genericRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(Customer customer)
        //{
        //    return genericRepository.Exists(customer);
        //}
    }
}