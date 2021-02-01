using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;
using WebCanteen.Areas.Admin.Middleware;

namespace canteenVLU.Areas.Admin.Controllers
{
    [LoginVerification]

    public class OrderController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        // GET: Admin/Order
        public ActionResult Index()
        {
            var order = model.ORDERs.OrderByDescending(x => x.ID).ToList();
            return View(order);
        }

        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.CUSTOMER_ID = model.CUSTOMERs.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(ORDER order)
        {
            if (ModelState.IsValid)
            {
                model.ORDERs.Add(order);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.CUSTOMER_ID = model.CUSTOMERs.OrderByDescending(x => x.ID).ToList();
            return View(order);
        }

        public ActionResult Edit(int id)
        {
            var order = model.ORDERs.FirstOrDefault(x => x.ID == id);
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID == id).ToList();
            ViewBag.CUSTOMER_ID = model.CUSTOMERs.OrderByDescending(x => x.ID == id).ToList();
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(ORDER order)
        {
            if (ModelState.IsValid)
            {
                model.Entry(order).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var order = model.ORDERs.Find(id);
            return View(order);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {                    
            ORDER order = model.ORDERs.Find(id);
            model.ORDERs.Remove(order);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Print(int id)
        {
            var orderPrint = model.ORDERs.FirstOrDefault(x => x.ID == id);
            return View(orderPrint);
        }
    }
}