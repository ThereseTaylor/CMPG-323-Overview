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
    public class ProductsController : Controller
    {
        private IGenericRepository<Product> genericRepository;
        private IProductRepository productRepository =  null;

        public ProductsController(IGenericRepository<Product> repository, IProductRepository productRepository)
        {
            this.genericRepository = repository;
            this.productRepository = productRepository;
        }

        // GET: Products
        public ActionResult Index()
        {
            var results = genericRepository.GetAll().ToList();
            return View(results);
        }   

        //GET: Products/Stock
        //This method views all the product that have 0 units left in stock.
        public ActionResult Stock()
        {
            var results = productRepository.GetDepletedStock();
            return View(results);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || genericRepository.GetAll() == null)
            {
                return NotFound();
            }

            var result = genericRepository.GetAll().FirstOrDefault(p => p.ProductId == id);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.ProductId).ToList().LastOrDefault().ProductId;
            List<int> newList = new List<int>();
            newList.Add(lastId + 1);
            ViewData["ProductId"] = new SelectList(newList);
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ProductID, ProductName, ProductDescription, UnitsInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Insert(product);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.ProductId).ToList().LastOrDefault().ProductId;
            List<int> newList = new List<int>();
            newList.Add(lastId + 1);
            ViewData["ProductId"] = new SelectList(newList, product.ProductId);
            return View(product);
        }

        // GET: Product/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Product product = genericRepository.GetById(id);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("ProductId, ProductName, ProductDescription, UnitsInStock")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    genericRepository.Update(product);
                    genericRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
                return View(product);
            }

        }

        // GET: Products/Delete/5
        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Product product = genericRepository.GetById(id);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return Problem("Entity set 'SuperStoreContext.Product'  is null.");
            }

            var customer = genericRepository.GetById(id);

            if (customer != null)
            {
                genericRepository.Delete(id);
            }

            genericRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (genericRepository.GetAll()?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
