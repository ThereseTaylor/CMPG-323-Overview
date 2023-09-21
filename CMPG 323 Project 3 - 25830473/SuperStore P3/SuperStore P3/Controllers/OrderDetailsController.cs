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
    public class OrderDetailsController : Controller
    {
        private IGenericRepository<OrderDetail> genericRepository = null;

        //The repository is added to get the values of the foreign keys.
        private IGenericRepository<Order>? genericRepositoryO = null;
        private IGenericRepository<Product>? genericRepositoryP = null;

        public OrderDetailsController(IGenericRepository<OrderDetail> repository, IGenericRepository<Order> genericRepositoryO, IGenericRepository<Product>? genericRepositoryP)
        {
            this.genericRepository = repository;
            this.genericRepositoryO = genericRepositoryO;
            this.genericRepositoryP = genericRepositoryP;
        }

        // GET: OrderDetailss
        public ActionResult Index()
        {
            var orderDetail = genericRepository.GetAll().Include(o => o.Order).Include(o => o.Product).ToList();
            return View(orderDetail);
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }
            var orderDetail = genericRepository.GetAll().Include(o => o.Order).Include(o => o.Product).FirstOrDefault(m => m.OrderDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            //newList get the value of the next open ID, so that the user can't put one in that already exists.
            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.OrderDetailsId).ToList().LastOrDefault().OrderDetailsId + 1;
            List<int> newList = new List<int>();
            newList.Add(lastId);
            //This shows the ID to the user in the view
            ViewData["OrderDetailsId"] = new SelectList(newList);
            ViewData["OrderId"] = new SelectList(genericRepositoryO?.GetAll().ToList(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(genericRepositoryP?.GetAll().ToList(), "ProductId", "ProductId");
            return View();
        }

        // POST: OrderDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create([Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                genericRepository.Insert(orderDetail);
                genericRepository.Save();
                return RedirectToAction(nameof(Index));
            }

            var lastId = genericRepository.GetAll().ToList().OrderBy(i => i.OrderDetailsId).ToList().LastOrDefault().OrderDetailsId +1;
            List<int> newList = new List<int>();
            newList.Add(lastId);
            ViewData["OrderDetailsId"] = new SelectList(newList, orderDetail.OrderDetailsId);
            ViewData["OrderId"] = new SelectList(genericRepositoryO?.GetAll().ToList(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(genericRepositoryP?.GetAll().ToList(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || genericRepository.GetAll().ToList() == null)
            {
                return NotFound();
            }

            var orderDetail = genericRepository.GetById(id);

            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(genericRepositoryO?.GetAll().ToList(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(genericRepositoryP?.GetAll().ToList(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("OrderDetailsId,OrderId,ProductId,Quantity,Discount")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    genericRepository.Update(orderDetail);
                    genericRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailsId))
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
            ViewData["OrderId"] = new SelectList(genericRepositoryO?.GetAll().ToList(), "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductId"] = new SelectList(genericRepositoryP?.GetAll().ToList(), "ProductId", "ProductId", orderDetail.ProductId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || genericRepositoryO?.GetAll().ToList() == null)
            {
                return NotFound();
            }

            var orderDetail = genericRepository.GetAll().Include(o => o.Order).Include(o => o.Product).FirstOrDefault(m => m.OrderDetailsId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (genericRepository.GetAll().ToList() == null)
            {
                return Problem("Entity set 'SuperStoreContext.OrderDetails'  is null.");
            }
            var orderDetail = genericRepository.GetById(id);
            if (orderDetail != null)
            {
                genericRepository.Delete(id);
            }

            genericRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        //Method that checks whether the id that the user gives exists or not.
        private bool OrderDetailExists(int id)
        {
            return (genericRepository.GetAll()?.Any(e => e.OrderDetailsId == id)).GetValueOrDefault();
        }
    }
}
