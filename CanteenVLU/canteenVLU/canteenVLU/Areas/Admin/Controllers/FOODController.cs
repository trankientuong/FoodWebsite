using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using canteenVLU.Models;
using WebCanteen.Areas.Admin.Middleware;
namespace canteenVLU.Areas.Admin.Controllers
{
    [LoginVerification]

    public class FOODController : Controller
    {
        AD1Team5Entities model = new AD1Team5Entities();
        // GET: Admin/FOOD
        public ActionResult Index()
        {
            var food = model.FOODs.OrderByDescending(x => x.ID).ToList();
            return View(food);
        }



        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(FOOD f, HttpPostedFileBase picture)
        {

            ValidateProduct(f);
            if (ModelState.IsValid)
            {
                if (picture != null)
                {
                    var fileName = Path.GetFileName(picture.FileName);
                    var directoryToSave = Server.MapPath(Url.Content("~/Images/"));
                    var pathToSave = Path.Combine(directoryToSave, fileName);
                    picture.SaveAs(pathToSave);
                    f.IMAGE_URL = Path.Combine("/Images/", fileName);
                }

                model.FOODs.Add(f);
                model.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(f);

        }


        private void ValidateProduct(FOOD food)
        {
            if (food.PRICE < 0)
            {
                ModelState.AddModelError("PRICE", "PRICE is less than zero");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var food = model.FOODs.FirstOrDefault(x => x.ID == id);
            ViewBag.product_type = model.CATEGORies.OrderByDescending(x => x.ID).ToList();
            return View(food);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(FOOD f, HttpPostedFileBase picture)
        {

            var fileName = Path.GetFileName(picture.FileName);
            var directoryToSave = Server.MapPath(Url.Content("~/Images/"));
            var pathToSave = Path.Combine(directoryToSave, fileName);
            picture.SaveAs(pathToSave);
            f.IMAGE_URL = Path.Combine("/Images/", fileName);

            model.Entry(f).State = System.Data.Entity.EntityState.Modified;
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var food = model.FOODs.FirstOrDefault(x => x.ID == id);
            return View(food);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            var food = model.FOODs.FirstOrDefault(x => x.ID == id);
            model.FOODs.Remove(food);
            model.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var food = model.FOODs.FirstOrDefault(x => x.ID == id);
            return View(food);
        }
    }
}