﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;
using EcoPower_Logistics.Repository;
using System.Drawing;

namespace Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private IGenericRepository<Order> genericRepository = null;
        private IGenericRepository<Customer>? genericRepositoryC = null;

        public OrdersController(IGenericRepository<Order> repository, IGenericRepository<Customer>? genericRepositoryC)
        {
            this.genericRepository = repository;
            this.genericRepositoryC = genericRepositoryC;
        }

        // GET: Orders
        public ActionResult Index()
        {
            var results = genericRepository.GetAll().Include(c => c.Customer).ToList();
            return View(results); ;
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || genericRepository.GetAll() == null)
            {
                return NotFound();
            }

            var order = genericRepository.GetAll().Include(o => o.Customer).FirstOrDefault(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.OrderId).ToList().LastOrDefault().OrderId;
            ViewData["OrderId"] = lastId;
            ViewData["CustomerId"] = new SelectList(genericRepositoryC?.GetAll().ToList(), "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Insert(order);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.OrderId).ToList().LastOrDefault().OrderId;
            ViewData["CustomerId"] = new SelectList(genericRepositoryC?.GetAll().ToList(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);

        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            Order order = genericRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(genericRepositoryC?.GetAll().ToList(), "CustomerId", "CustomerId", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("OrderId,OrderDate,CustomerId,DeliveryAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    genericRepository.Update(order);
                    genericRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                ViewData["CustomerId"] = new SelectList(genericRepositoryC?.GetAll().ToList(), "CustomerId", "CustomerId", order.CustomerId);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(order);
            }
        }


        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            var order = genericRepository.GetAll().Include(o => o.Customer).FirstOrDefault(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (genericRepository.GetAll().ToList() == null)
            {
                return Problem("Entity set 'SuperStoreContext.Orders'  is null.");
            }

            var order = genericRepository.GetById(id);

            if (order != null)
            {
                genericRepository.Delete(id);
            }

            genericRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return (genericRepository.GetAll()?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
