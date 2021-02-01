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
    public class MENUController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        // GET: Admin/MENU
        public ActionResult Index()
        {
            var menu = model.MENUs.OrderByDescending(x => x.ID).ToList();
            return View(menu);
        }

        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.FOOD_NAME = model.FOODs.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MENU menu)
        {
            if (ModelState.IsValid)
            {
                model.MENUs.Add(menu);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.FOOD_ID = model.FOODs.OrderByDescending(x => x.ID).ToList();
            return View(menu);
        }

        public ActionResult Edit(int id)
        {
            var menu = model.MENUs.FirstOrDefault(x => x.ID == id);
            ViewBag.ACCOUNT_ID = model.ACCOUNTs.OrderByDescending(x => x.ID).ToList();
            ViewBag.FOOD_NAME = model.FOODs.OrderByDescending(x => x.ID).ToList();
            return View(menu);
        }

        [HttpPost]
        public ActionResult Edit(MENU menu)
        {
            if (ModelState.IsValid)
            {
                model.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                model.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var menu = model.MENUs.FirstOrDefault(x => x.ID == id);
            return View(menu);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var menu = model.MENUs.FirstOrDefault(x => x.ID == id);
            model.MENUs.Remove(menu);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var menu = model.MENUs.FirstOrDefault(x => x.ID == id);
            return View(menu);
        }

    }
}